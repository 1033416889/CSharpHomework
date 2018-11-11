<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/ArrayOfOrder">
		<html>
			<head>
				<title>Array Of Order</title>
			</head>
			<body>
				<ul>
					<xsl:for-each select="Order">
						<li>
							订单ID：<xsl:value-of select="Id" />
							<ul>
								<li>
									客户信息：
									<ul>
										<xsl:for-each select="Customer">
										<li>
											客户ID：<xsl:value-of select="Id" />
										</li>
										<li>
											客户名：<xsl:value-of select="Name" />
										</li>
										</xsl:for-each>
									</ul>
								</li>
								<li>
									订单明细：
									<xsl:for-each select="Details">
									<xsl:for-each select="OrderDetail">
									<ul><li>
										<xsl:for-each select="Goods">
											商品名：<xsl:value-of select="Name" /> 价格：<xsl:value-of select="Price" />										
										</xsl:for-each>
										数量：<xsl:value-of select="Quantity" />
									</li></ul>
									</xsl:for-each>
									</xsl:for-each>
								</li>
							</ul>
							
						</li>
					</xsl:for-each>
				</ul>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
