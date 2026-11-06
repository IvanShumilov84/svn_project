
KineticaRTOPCps.dll: dlldata.obj KineticaRTOPC_p.obj KineticaRTOPC_i.obj
	link /dll /out:KineticaRTOPCps.dll /def:KineticaRTOPCps.def /entry:DllMain dlldata.obj KineticaRTOPC_p.obj KineticaRTOPC_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del KineticaRTOPCps.dll
	@del KineticaRTOPCps.lib
	@del KineticaRTOPCps.exp
	@del dlldata.obj
	@del KineticaRTOPC_p.obj
	@del KineticaRTOPC_i.obj
