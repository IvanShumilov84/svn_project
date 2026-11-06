<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
    <head>
      <title>Test Progress Sheet</title>
      <link rel="stylesheet" type="text/css" href="C:\My Projects\CELDAS 200\HMI\testprogresssheet.css"/>
    </head>
    <body>
      <xsl:apply-templates select="TestProgressSheet"/>
    </body>
  </html>
</xsl:template>

<xsl:template match="TestProgressSheet">
  <div class="page_header">
    <table>
      <tr>
        <td class="double-ltr-spacing"><br/>TEI</td>
        <td class="middle-center">Test Progress</td>
        <td>
          <ul>
            <li>Engine SN: <xsl:value-of select="EngineSN"/></li>
            <li>Type: <xsl:value-of select="EngineModel"/></li>
            <li>Work Order: <xsl:value-of select="WorkOrder"/></li>
            <li>Date: <xsl:value-of select="DateTime/Year"/>/<xsl:value-of select="DateTime/Month"/>/<xsl:value-of select="DateTime/Day"/></li>
          </ul>
        </td>
      </tr>
    </table>
  </div>
  <xsl:apply-templates select="Header"/>
  <xsl:apply-templates select="Entries"/>
</xsl:template>

  <xsl:template match="Header">
    <div class="headerEntries">
    <table>
      <xsl:call-template name="displayHeaderEntry">
        <xsl:with-param name ="pStart" select="1"/>
        <xsl:with-param name ="pCount" select="count(HeaderEntry)"/>
      </xsl:call-template>
    </table>
    </div>
  </xsl:template>

  <xsl:template name="displayHeaderEntry">
    <xsl:param name="pStart"/>
    <xsl:param name="pCount"/>
  
    <xsl:if test="not($pStart > ceiling($pCount div 2))">
      <tr>
        <xsl:if test="HeaderEntry[$pStart]">
          <td class="HeaderKey">
            <xsl:value-of select="HeaderEntry[$pStart]/@Key"/>
          </td>
          <td class ="HeaderValue">
            : <xsl:value-of select="HeaderEntry[$pStart]/@Value"/>
          </td>
        </xsl:if>
        <xsl:if test="HeaderEntry[$pStart+1]">
          <td class="HeaderKey">
            <xsl:value-of select="HeaderEntry[$pStart + ($pCount div 2)]/@Key"/>
          </td>
          <td class="HeaderValue">
            : <xsl:value-of select="HeaderEntry[$pStart + ($pCount div 2)]/@Value"/>
          </td>
        </xsl:if>
      </tr>
      <xsl:call-template name="displayHeaderEntry">
        <xsl:with-param name ="pStart" select="$pStart + 1"/>
        <xsl:with-param name ="pCount" select="$pCount"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
    
<xsl:template match="Entries">
  <div class="entries">
    <table>
      <xsl:apply-templates/>
    </table>
  </div>
</xsl:template>

<xsl:template match="TestProgressSheetEntry">
  <tr>
    <td class="entrytime">
      <xsl:apply-templates select="DateTime"/>
      <xsl:if test="DateTime != ''"> : </xsl:if>
    </td>
    <td>
      <ul>
        <li>
          <xsl:apply-templates select="Result"/>
          <ul class="sub-list">
            <xsl:apply-templates select="CheckItemResults/CheckItem"/>
          </ul>
        </li>
      </ul>
    </td>
  </tr>
</xsl:template>


<xsl:template match="CheckItemResults/CheckItem">
    <li><xsl:value-of select="."/></li>
</xsl:template>

</xsl:stylesheet>
