using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Service.Assets.Emails_Layout
{
    public static class TemplateManager
    {
        public static string GetPasswordResetEmailTemplate(string url)
        {
            return $@"
                <!DOCTYPE html>

					<html lang=""en"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml"">
					<head>
					<title></title>
					<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type""/>
					<meta content=""width=device-width, initial-scale=1.0"" name=""viewport""/><!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]--><!--[if !mso]><!--><!--<![endif]-->
					<style>
							* {{
								box-sizing: border-box;
							}}

							body {{
								margin: 0;
								padding: 0;
							}}

							a[x-apple-data-detectors] {{
								color: inherit !important;
								text-decoration: inherit !important;
							}}

							#MessageViewBody a {{
								color: inherit;
								text-decoration: none;
							}}

							p {{
								line-height: inherit
							}}

							.desktop_hide,
							.desktop_hide table {{
								mso-hide: all;
								display: none;
								max-height: 0px;
								overflow: hidden;
							}}

							.image_block img+div {{
								display: none;
							}}

							@media (max-width:690px) {{

								.desktop_hide table.icons-inner,
								.social_block.desktop_hide .social-table {{
									display: inline-block !important;
								}}

								.icons-inner {{
									text-align: center;
								}}

								.icons-inner td {{
									margin: 0 auto;
								}}

								.mobile_hide {{
									display: none;
								}}

								.row-content {{
									width: 100% !important;
								}}

								.stack .column {{
									width: 100%;
									display: block;
								}}

								.mobile_hide {{
									min-height: 0;
									max-height: 0;
									max-width: 0;
									overflow: hidden;
									font-size: 0px;
								}}

								.desktop_hide,
								.desktop_hide table {{
									display: table !important;
									max-height: none !important;
								}}
							}}
						</style>
					</head>
					<body class=""body"" style=""background-color: #37474f; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #37474f;"" width=""100%"">
					<tbody>
					<tr>
					<td>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tbody>
					<tr>
					<td>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #1f1f20; border-radius: 0; color: #000000; width: 670px; margin: 0 auto;"" width=""670"">
					<tbody>
					<tr>
					<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""16.666666666666668%"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""image_block block-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-bottom:20px;padding-left:20px;padding-right:10px;padding-top:20px;width:100%;"">
					<div align=""right"" class=""alignment"" style=""line-height:10px"">
					<div style=""max-width: 111.667px;""><a href=""www.example.com"" style=""outline:none"" tabindex=""-1"" target=""_blank""><img alt=""InsurTech Logo"" height=""auto"" src=""https://i.ibb.co/wKc3VWV/logo.png"" style=""display: block; height: auto; border: 0; width: 100%;"" title=""InsurTech Logo"" width=""111.667""/></a></div>
					</div>
					</td>
					</tr>
					</table>
					</td>
					<td class=""column column-2"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""33.333333333333336%"">
					<table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""heading_block block-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"">
					<h1 style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 39px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 46.8px;""><strong>InsurTech<br/></strong></h1>
					</td>
					</tr>
					</table>
					</td>
					<td class=""column column-3"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""50%"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""heading_block block-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""text-align:center;width:100%;"">
					<h1 style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 30px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 36px;""><span class=""tinyMce-placeholder"">Forgot Something?</span></h1>
					</td>
					</tr>
					</table>
					</td>
					</tr>
					</tbody>
					</table>
					</td>
					</tr>
					</tbody>
					</table>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tbody>
					<tr>
					<td>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #b1e5db; color: #000000; width: 670px; margin: 0 auto;"" width=""670"">
					<tbody>
					<tr>
					<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""image_block block-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-top:20px;width:100%;"">
					<div align=""center"" class=""alignment"" style=""line-height:10px"">
					<div style=""max-width: 670px;""><a href=""www.example.com"" style=""outline:none"" tabindex=""-1"" target=""_blank""><img alt=""reset password"" height=""auto"" src=""https://i.ibb.co/X5Lf6pB/3275432.png"" style=""display: block; height: auto; border: 0; width: 100%;"" title=""reset password"" width=""670""/></a></div>
					</div>
					</td>
					</tr>
					</table>
					<div class=""spacer_block block-2"" style=""height:40px;line-height:40px;font-size:1px;""> </div>
					<table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""paragraph_block block-3"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%"">
					<tr>
					<td class=""pad"">
					<div style=""color:#393d47;font-family:'Helvetica Neue',Helvetica,Arial,sans-serif;font-size:20px;line-height:150%;text-align:center;mso-line-height-alt:30px;"">
					<p style=""margin: 0; word-break: break-word;""><strong>We received a request to reset your password.</strong></p>
					<p style=""margin: 0; word-break: break-word;""><strong>If you didn't make this request, simply ignore this email.</strong></p>
					</div>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""20"" cellspacing=""0"" class=""button_block block-4"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"">
					<div align=""center"" class=""alignment""><!--[if mso]>
					<v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" href=""www.example.com"" style=""height:48px;width:98px;v-text-anchor:middle;"" arcsize=""50%"" stroke=""false"" fillcolor=""#37474f"">
					<w:anchorlock/>
					<v:textbox inset=""0px,0px,0px,0px"">
					<center dir=""false"" style=""color:#ffffff;font-family:Arial, sans-serif;font-size:19px"">
					<![endif]--><a href=""{url}"" style=""background-color:#37474f;border-bottom:0px solid transparent;border-left:0px solid transparent;border-radius:24px;border-right:0px solid transparent;border-top:0px solid transparent;color:#ffffff;display:inline-block;font-family:'Helvetica Neue', Helvetica, Arial, sans-serif;font-size:19px;font-weight:undefined;mso-border-alt:none;padding-bottom:5px;padding-top:5px;text-align:center;text-decoration:none;width:auto;word-break:keep-all;"" target=""_blank""><span style=""padding-left:15px;padding-right:15px;font-size:19px;display:inline-block;letter-spacing:1px;""><span style=""word-break: break-word; line-height: 38px;""><strong>RESET</strong></span></span></a><!--[if mso]></center></v:textbox></v:roundrect><![endif]--></div>
					</td>
					</tr>
					</table>
					</td>
					</tr>
					</tbody>
					</table>
					</td>
					</tr>
					</tbody>
					</table>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-3"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tbody>
					<tr>
					<td>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #1f1f20; color: #000000; width: 670px; margin: 0 auto;"" width=""670"">
					<tbody>
					<tr>
					<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""33.333333333333336%"">
					<div class=""spacer_block block-1"" style=""height:20px;line-height:20px;font-size:1px;""> </div>
					<table border=""0"" cellpadding=""25"" cellspacing=""0"" class=""image_block block-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"">
					<div align=""center"" class=""alignment"" style=""line-height:10px"">
					<div style=""max-width: 89.333px;""><a href=""www.example.com"" style=""outline:none"" tabindex=""-1"" target=""_blank""><img alt=""company logo"" height=""auto"" src=""https://i.ibb.co/wKc3VWV/logo.png"" style=""display: block; height: auto; border: 0; width: 100%;"" title=""company logo"" width=""89.333""/></a></div>
					</div>
					</td>
					</tr>
					</table>
					<div class=""spacer_block block-3"" style=""height:20px;line-height:20px;font-size:1px;""> </div>
					</td>
					<td class=""column column-2"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""33.333333333333336%"">
					<div class=""spacer_block block-1"" style=""height:20px;line-height:20px;font-size:1px;""> </div>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""heading_block block-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-left:20px;text-align:center;width:100%;"">
					<h3 style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; line-height: 200%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 32px;""><strong>About Us</strong></h3>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""divider_block block-3"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"">
					<div align=""left"" class=""alignment"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""80%"">
					<tr>
					<td class=""divider_inner"" style=""font-size: 1px; line-height: 1px; border-top: 2px solid #BBBBBB;""><span> </span></td>
					</tr>
					</table>
					</div>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""paragraph_block block-4"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
					<div style=""color:#ffffff;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:12px;line-height:150%;text-align:left;mso-line-height-alt:18px;"">
					<p style=""margin: 0; word-break: break-word;""><span>InsurTech is an innovative online marketplace designed to simplify and streamline the process of exploring, comparing, and purchasing insurance products.<br/></span></p>
					</div>
					</td>
					</tr>
					</table>
					</td>
					<td class=""column column-3"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""33.333333333333336%"">
					<div class=""spacer_block block-1"" style=""height:20px;line-height:20px;font-size:1px;""> </div>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""heading_block block-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-left:20px;text-align:center;width:100%;"">
					<h3 style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; line-height: 200%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 32px;""><strong>Contact</strong></h3>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""divider_block block-3"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"">
					<div align=""left"" class=""alignment"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""80%"">
					<tr>
					<td class=""divider_inner"" style=""font-size: 1px; line-height: 1px; border-top: 2px solid #BBBBBB;""><span> </span></td>
					</tr>
					</table>
					</div>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""paragraph_block block-4"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
					<div style=""color:#a9a9a9;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;line-height:120%;text-align:left;mso-line-height-alt:16.8px;"">
					<p style=""margin: 0; word-break: break-word;""><a rel=""noopener"" style=""text-decoration: none; color: #e9e7e7;"" target=""_blank"">support@InsurTech.com</a></p>
					</div>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""paragraph_block block-5"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
					<div style=""color:#a9a9a9;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;line-height:120%;text-align:left;mso-line-height-alt:16.8px;"">
					<p style=""margin: 0; word-break: break-word;""><a rel=""noopener"" style=""text-decoration: none; color: #e9e7e7;"" target=""_blank"">Our Location</a></p>
					</div>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""paragraph_block block-6"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
					<div style=""color:#a9a9a9;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;line-height:120%;text-align:left;mso-line-height-alt:16.8px;"">
					<p style=""margin: 0; word-break: break-word;""><a rel=""noopener"" style=""text-decoration: underline; color: #e9e7e7;"" target=""_blank"">Unsubscribe</a></p>
					</div>
					</td>
					</tr>
					</table>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""social_block block-7"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
					<tr>
					<td class=""pad"" style=""padding-bottom:10px;padding-left:20px;padding-right:10px;padding-top:10px;text-align:left;"">
					<div align=""left"" class=""alignment"">
					</div>
					</td>
					</tr>
					</table>
					<div class=""spacer_block block-8"" style=""height:20px;line-height:20px;font-size:1px;""> </div>
					</td>
					</tr>
					</tbody>
					</table>
					</td>
					</tr>
					</tbody>
					</table>
					<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-4"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #ffffff;"" width=""100%"">
					<tbody>
					<tr>
					<td>
					</td>
					</tr>
					</tbody>
					</table>
					</td>
					</tr>
					</tbody>
					</table><!-- End -->
					</body>
					</html>
            ";
        }

        public static string GetConfirmationEmailTemplate(string url)
        {
			return $@"
				<html lang=""en"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml"">

				<head>
					<title></title>
					<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
					<meta content=""width=device-width, initial-scale=1.0"" name=""viewport"">
					<!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]--><!--[if !mso]><!--><!--<![endif]-->
					<style>
						* {{
							box-sizing: border-box;
						}}

						body {{
							margin: 0;
							padding: 0;
						}}

						a[x-apple-data-detectors] {{
							color: inherit !important;
							text-decoration: inherit !important;
						}}

						#MessageViewBody a {{
							color: inherit;
							text-decoration: none;
						}}

						p {{
							line-height: inherit
						}}

						.desktop_hide,
						.desktop_hide table {{
							mso-hide: all;
							display: none;
							max-height: 0px;
							overflow: hidden;
						}}

						.image_block img+div {{
							display: none;
						}}

						@media (max-width:690px) {{

							.desktop_hide table.icons-inner,
							.social_block.desktop_hide .social-table {{
								display: inline-block !important;
							}}

							.icons-inner {{
								text-align: center;
							}}

							.icons-inner td {{
								margin: 0 auto;
							}}

							.mobile_hide {{
								display: none;
							}}

							.row-content {{
								width: 100% !important;
							}}

							.stack .column {{
								width: 100%;
								display: block;
							}}

							.mobile_hide {{
								min-height: 0;
								max-height: 0;
								max-width: 0;
								overflow: hidden;
								font-size: 0px;
							}}

							.desktop_hide,
							.desktop_hide table {{
								display: table !important;
								max-height: none !important;
							}}
						}}
					</style>
				</head>

				<body class=""body""
					style=""background-color: #37474f; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;"">
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation""
						style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #37474f;"" width=""100%"">
						<tbody>
							<tr>
								<td>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-1""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #1f1f20; border-radius: 0; color: #000000; width: 670px; margin: 0 auto;""
														width=""670"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""16.666666666666668%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""image_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:20px;padding-left:20px;padding-right:10px;padding-top:20px;width:100%;"">
																					<div align=""right"" class=""alignment""
																						style=""line-height:10px"">
																						<div style=""max-width: 111.667px;""><a
																								href=""www.example.com""
																								style=""outline:none"" tabindex=""-1""
																								target=""_blank""><img
																									alt=""InsurTech Logo"" height=""auto""
																									src=""https://i.ibb.co/wKc3VWV/logo.png""
																									style=""display: block; height: auto; border: 0; width: 100%;""
																									title=""InsurTech Logo""
																									width=""111.667""></a></div>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</td>
																<td class=""column column-2""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""heading_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"">
																					<h1
																						style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 39px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 46.8px;"">
																						<strong>InsurTech<br></strong>
																					</h1>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</td>
																<td class=""column column-3""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: middle; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""50%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"" style=""text-align:center;width:100%;"">
																					<h1
																						style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 30px; font-weight: 700; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 36px;"">
																						<span class=""tinyMce-placeholder"">Welcome
																							😄</span>
																					</h1>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-2""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #b1e5db; color: #000000; width: 670px; margin: 0 auto;""
														width=""670"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""100%"">
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""image_block block-1"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"" style=""padding-top:20px;width:100%;"">
																					<div align=""center"" class=""alignment""
																						style=""line-height:10px"">
																						<div style=""max-width: 670px;""><a
																								href=""www.example.com""
																								style=""outline:none"" tabindex=""-1""
																								target=""_blank""><img
																									alt=""reset password"" height=""auto""
																									src=""https://cdn-icons-png.flaticon.com/512/8662/8662513.png""
																									style=""display: block; height: auto; border: 0; width: 50%;""
																									title=""reset password""
																									width=""670""></a></div>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<div class=""spacer_block block-2""
																		style=""height:40px;line-height:40px;font-size:1px;"">&hairsp;
																	</div>
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""paragraph_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"">
																					<div
																						style=""color:#393d47;font-family:'Helvetica Neue',Helvetica,Arial,sans-serif;font-size:20px;line-height:150%;text-align:center;mso-line-height-alt:30px;"">
																						<p style=""margin: 0; word-break: break-word;"">
																							<strong>We're excited to have you get
																								started. You need to confirm your
																								account, Just press the button
																								below</strong>
																						</p>
																						<table border=""0"" cellpadding=""20"" cellspacing=""0"" class=""button_block block-4"" role=""presentation""
																							style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
																							<tbody>
																								<tr>
																									<td class=""pad"">
																										<div align=""center"" class=""alignment""><!--[if mso]>
																						<v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" href=""www.example.com"" style=""height:48px;width:98px;v-text-anchor:middle;"" arcsize=""50%"" stroke=""false"" fillcolor=""#37474f"">
																						<w:anchorlock/>
																						<v:textbox inset=""0px,0px,0px,0px"">
																						<center dir=""false"" style=""color:#ffffff;font-family:Arial, sans-serif;font-size:19px"">
																						<![endif]--><a href=""{url}""
																												style=""background-color:#37474f;border-bottom:0px solid transparent;border-left:0px solid transparent;border-radius:24px;border-right:0px solid transparent;border-top:0px solid transparent;color:#ffffff;display:inline-block;font-family:'Helvetica Neue', Helvetica, Arial, sans-serif;font-size:19px;font-weight:undefined;mso-border-alt:none;padding-bottom:5px;padding-top:5px;text-align:center;text-decoration:none;width:auto;word-break:keep-all;""
																												target=""_blank""><span
																													style=""padding-left:15px;padding-right:15px;font-size:19px;display:inline-block;letter-spacing:1px;""><span
																														style=""word-break: break-word; line-height: 38px;""><strong>CONFIRM
																															ACCOUNT</strong></span></span></a><!--[if mso]></center></v:textbox></v:roundrect><![endif]-->
																										</div>
																									</td>
																								</tr>
																							</tbody>
																						</table>
																						<p style=""margin: 0; word-break: break-word;"">
																							<strong>If you didn't make this request,
																								simply ignore this email.</strong>
																						</p>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
													
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-3""
										role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
										<tbody>
											<tr>
												<td>
													<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""
														class=""row-content stack"" role=""presentation""
														style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #1f1f20; color: #000000; width: 670px; margin: 0 auto;""
														width=""670"">
														<tbody>
															<tr>
																<td class=""column column-1""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<div class=""spacer_block block-1""
																		style=""height:20px;line-height:20px;font-size:1px;"">&hairsp;
																	</div>
																	<table border=""0"" cellpadding=""25"" cellspacing=""0""
																		class=""image_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"">
																					<div align=""center"" class=""alignment""
																						style=""line-height:10px"">
																						<div style=""max-width: 89.333px;""><a
																								href=""www.example.com""
																								style=""outline:none"" tabindex=""-1""
																								target=""_blank""><img alt=""company logo""
																									height=""auto""
																									src=""https://i.ibb.co/wKc3VWV/logo.png""
																									style=""display: block; height: auto; border: 0; width: 100%;""
																									title=""company logo""
																									width=""89.333""></a></div>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<div class=""spacer_block block-3""
																		style=""height:20px;line-height:20px;font-size:1px;"">&hairsp;
																	</div>
																</td>
																<td class=""column column-2""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<div class=""spacer_block block-1""
																		style=""height:20px;line-height:20px;font-size:1px;"">&hairsp;
																	</div>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-left:20px;text-align:center;width:100%;"">
																					<h3
																						style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; line-height: 200%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 32px;"">
																						<strong>About Us</strong>
																					</h3>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""divider_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"">
																					<div align=""left"" class=""alignment"">
																						<table border=""0"" cellpadding=""0""
																							cellspacing=""0"" role=""presentation""
																							style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																							width=""80%"">
																							<tbody>
																								<tr>
																									<td class=""divider_inner""
																										style=""font-size: 1px; line-height: 1px; border-top: 2px solid #BBBBBB;"">
																										<span>&hairsp;</span>
																									</td>
																								</tr>
																							</tbody>
																						</table>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-4"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
																					<div
																						style=""color:#ffffff;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:12px;line-height:150%;text-align:left;mso-line-height-alt:18px;"">
																						<p style=""margin: 0; word-break: break-word;"">
																							<span>InsurTech is an innovative online
																								marketplace designed to simplify and
																								streamline the process of exploring,
																								comparing, and purchasing insurance
																								products.<br></span>
																						</p>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</td>
																<td class=""column column-3""
																	style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; padding-bottom: 5px; padding-top: 5px; vertical-align: top; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;""
																	width=""33.333333333333336%"">
																	<div class=""spacer_block block-1""
																		style=""height:20px;line-height:20px;font-size:1px;"">&hairsp;
																	</div>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""heading_block block-2"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-left:20px;text-align:center;width:100%;"">
																					<h3
																						style=""margin: 0; color: #ffffff; direction: ltr; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; line-height: 200%; text-align: left; margin-top: 0; margin-bottom: 0; mso-line-height-alt: 32px;"">
																						<strong>Contact</strong>
																					</h3>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""10"" cellspacing=""0""
																		class=""divider_block block-3"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad"">
																					<div align=""left"" class=""alignment"">
																						<table border=""0"" cellpadding=""0""
																							cellspacing=""0"" role=""presentation""
																							style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																							width=""80%"">
																							<tbody>
																								<tr>
																									<td class=""divider_inner""
																										style=""font-size: 1px; line-height: 1px; border-top: 2px solid #BBBBBB;"">
																										<span>&hairsp;</span>
																									</td>
																								</tr>
																							</tbody>
																						</table>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-4"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
																					<div
																						style=""color:#a9a9a9;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;line-height:120%;text-align:left;mso-line-height-alt:16.8px;"">
																						<p style=""margin: 0; word-break: break-word;""><a
																								href=""http://www.example.com""
																								rel=""noopener""
																								style=""text-decoration: none; color: #e9e7e7;""
																								target=""_blank"">support@InsurTech.com</a>
																						</p>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-5"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
																					<div
																						style=""color:#a9a9a9;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;line-height:120%;text-align:left;mso-line-height-alt:16.8px;"">
																						<p style=""margin: 0; word-break: break-word;""><a
																								href=""http://www.example.com""
																								rel=""noopener""
																								style=""text-decoration: none; color: #e9e7e7;""
																								target=""_blank"">Our Location</a></p>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""paragraph_block block-6"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:20px;padding-right:20px;padding-top:10px;"">
																					<div
																						style=""color:#a9a9a9;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;line-height:120%;text-align:left;mso-line-height-alt:16.8px;"">
																						<p style=""margin: 0; word-break: break-word;""><a
																								href=""http://www.example.com""
																								rel=""noopener""
																								style=""text-decoration: underline; color: #e9e7e7;""
																								target=""_blank"">Unsubscribe</a></p>
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<table border=""0"" cellpadding=""0"" cellspacing=""0""
																		class=""social_block block-7"" role=""presentation""
																		style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;""
																		width=""100%"">
																		<tbody>
																			<tr>
																				<td class=""pad""
																					style=""padding-bottom:10px;padding-left:20px;padding-right:10px;padding-top:10px;text-align:left;"">
																					<div align=""left"" class=""alignment"">
																					</div>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																	<div class=""spacer_block block-8""
																		style=""height:20px;line-height:20px;font-size:1px;"">&hairsp;
																	</div>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
									<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-4""
										role=""presentation""
										style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #ffffff;"" width=""100%"">
										<tbody>
											<tr>
												<td>
												</td>
											</tr>
										</tbody>
									</table>
								</td>
							</tr>
						</tbody>
					</table>
				</body>

				</html>
				";
        }
    }
}
