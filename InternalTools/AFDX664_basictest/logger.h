#ifndef LOGGER_H
#define LOGGER_H

// RAMLOG
#define MAX_RAMLOG 128*1024*1024
extern char ramlog[MAX_RAMLOG];
extern  unsigned long ramndx;
extern LARGE_INTEGER start_tick;

// TIMING
extern LARGE_INTEGER hpet_freq;
extern LARGE_INTEGER tick;
LARGE_INTEGER tock;

#define QueryPerfCounter(var) { if ( QueryPerformanceCounter(&var) == FALSE) {printf("?"); error_found=TRUE; goto finish_up;}}

__inline LONGLONG usleep (int micro_seconds)
{
    LARGE_INTEGER now_tick;
    LONGLONG diff;
    // Initialize real-time tick
    QueryPerformanceCounter(&start_tick);  // in counts
    QueryPerformanceCounter(&now_tick);

    do {
        QueryPerformanceCounter(&now_tick);
        diff = (now_tick.QuadPart - start_tick.QuadPart + 1) * 1000000 / hpet_freq.QuadPart;
    } while( diff < micro_seconds );

    return diff;
}

__inline LONGLONG udiff (LARGE_INTEGER t1, LARGE_INTEGER t2 )
{
	if (hpet_freq.QuadPart)
		return (t2.QuadPart - t1.QuadPart) * 1000000 / hpet_freq.QuadPart; // difference in µs
	return 0;
}

__inline double mdiff (LARGE_INTEGER t1, LARGE_INTEGER t2 )
{
    double diff = (double)((t2.QuadPart - t1.QuadPart) * 1000000000 / hpet_freq.QuadPart);
    return diff/1000000;
}

// String printing
__inline int really_write_ramlog (char *str, int wrap)
{
int len;
    len = (int) strlen(str);
    if ((ramndx + len) < MAX_RAMLOG) {
        memcpy(&ramlog[ramndx], str, len);
        ramndx += len;
    }
    else if (wrap)
    {
        memcpy(&ramlog[0], str, len);
        ramndx = len;
    }
    else
    {
        printf("Log Full\n");
        return 0;
    }
    return 1;
}

__inline int write_ramlog(char *str)
{
    int wrap = TRUE; // 0 = don't wrap, just close
    static LARGE_INTEGER start_tick;
    LARGE_INTEGER stop_tick;
    char usec_tick[40];
    if (ramndx == 0)
    {
        QueryPerformanceCounter(&start_tick);
    }
    QueryPerformanceCounter(&stop_tick);
    /* Convert to usec */
    sprintf(usec_tick, "%0.3f ms: ", ((float)udiff(start_tick, stop_tick))/1000);
    really_write_ramlog(usec_tick, wrap);
    //printf("log (%s): %s",usec_tick, str);
    return really_write_ramlog(str, wrap);
}

__inline void print_ramlog_length()
{
	printf("Saving %d bytes to file.\n",ramndx);
}

__inline void save_ramlog (char *fname)
{
FILE *fp;
    fp = fopen(fname, "w");

    if (fp == NULL) {
        printf("Unable to open output file for writing\n");
        return;
    }

    fwrite(ramlog, ramndx, 1, fp);
    fclose(fp);
}

__inline void init_hpet()
{
    // Force run realtime on a single core
    SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);
    SetThreadAffinityMask(GetCurrentThread(), 1);

    // get win32 "High-performance Event Timer" frequency
    QueryPerformanceFrequency(&hpet_freq);
    
    if (hpet_freq.QuadPart == 0)
        printf("Cannot get hpet_frequency! You should reboot!\n");
}

__inline void printf_ramlog(const char* format, ...)
{
    char str[65536];

    va_list argptr;
    va_start(argptr, format);
    vsnprintf(str, sizeof(str)/sizeof(char), format, argptr);
    va_end(argptr);

    write_ramlog(str);

}

#endif