<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="login2011Browser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  <meta name="viewport" content="width=device-width, initial-scale=1">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server" >

        <asp:HiddenField ID="HiddenMobile" runat="server" Value="0" />

    <%--<div class="login-layout">--%>

		<div class="main-container">
			<div class="main-content">
				<div class="row">
					<div class="col-sm-10 col-sm-offset-1">
						<div class="login-container">
							<div class="center">
								<h1>
									<i class="ace-icon fa fa-leaf green"></i>
									<span class="red">PTS</span>
<%--									<span class="grey" id="id-text2">Procurement Tracing System</span>--%>
								</h1>
								<h6 class="blue" id="id-company-text">&copy; Mercury Engineering</h6>
							</div>

							<div class="space-6"></div>

							<div class="position-relative">
								<div id="login-box" class="login-box visible widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header blue lighter bigger">
												<i class="ace-icon fa fa-coffee green"></i>
												Please Enter Your Information
											</h4>

											<div class="space-6"></div>
                                            <asp:Panel ID="panelLogin" runat="server" DefaultButton="ButtonLogin">
											<form>
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
                                                            <asp:TextBox ID="TextBoxUser" runat="server" class="form-control" placeholder="User Name"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUser" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxUser" Display="Dynamic"></asp:RequiredFieldValidator>

															<i class="ace-icon fa fa-user"></i>
														</span>
													</label>

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
                                                            <asp:TextBox ID="TextBoxPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Required" ControlToValidate="TextBoxPassword" Display="Dynamic"></asp:RequiredFieldValidator>
															<i class="ace-icon fa fa-lock"></i>
														</span>
													</label>

													<div class="space"></div>

													<div class="clearfix">
<%--														<label class="inline">
															<input type="checkbox" class="ace" />
															<span class="lbl"> Remember Me</span>
														</label>--%>

                                                        <asp:LinkButton ID="ButtonLogin" runat="server" CssClass="width-35 pull-right btn btn-sm btn-primary" Text="Login" OnClick="ButtonLogin_Click" >
                                                            <i class="ace-icon fa fa-key"></i>
                                                            <span class="bigger-110">Login</span>
                                                        </asp:LinkButton>

													</div>

													<div class="space-4"></div>
												</fieldset>
											</form>
                                            </asp:Panel>
                                              <div id="DivMessage" class="alert alert-danger" runat="server" visible="false">
                                                  <button class="close" type="button" data-dismiss="alert">×</button>
                                                  <strong>Sorry!</strong> Login failed. Please try again.
                                              </div>


<%--											<div class="social-or-login center">
												<span class="bigger-110">Or Login Using</span>
											</div>

											<div class="space-6"></div>

											<div class="social-login center">
												<a class="btn btn-primary">
													<i class="ace-icon fa fa-facebook"></i>
												</a>

												<a class="btn btn-info">
													<i class="ace-icon fa fa-twitter"></i>
												</a>

												<a class="btn btn-danger">
													<i class="ace-icon fa fa-google-plus"></i>
												</a>
											</div>--%>
										</div><!-- /.widget-main -->

<%--										<div class="toolbar clearfix">
											<div>
												<a href="#" data-target="#forgot-box" class="forgot-password-link">
													<i class="ace-icon fa fa-arrow-left"></i>
													I forgot my password
												</a>
											</div>

											<div>
												<a href="#" data-target="#signup-box" class="user-signup-link">
													I want to register
													<i class="ace-icon fa fa-arrow-right"></i>
												</a>
											</div>
										</div>--%>
									</div><!-- /.widget-body -->
								</div><!-- /.login-box -->

							</div><!-- /.position-relative -->

<%--							<div class="navbar-fixed-top align-right">
								<br />
								&nbsp;
								<a id="btn-login-dark" href="#">Dark</a>
								&nbsp;
								<span class="blue">/</span>
								&nbsp;
								<a id="btn-login-blur" href="#">Blur</a>
								&nbsp;
								<span class="blue">/</span>
								&nbsp;
								<a id="btn-login-light" href="#">Light</a>
								&nbsp; &nbsp; &nbsp;
							</div>--%>
						</div>
					</div><!-- /.col -->
				</div><!-- /.row -->
			</div><!-- /.main-content -->
		</div><!-- /.main-container -->

    <%--</div>--%>
    
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

    <script type="text/javascript">

        $(function () {

            var s = $("input[name*='HiddenMobile']").val();
            if (s == "1") {
                $("#navbar").css('visibility', 'hidden');
            }

        })

    </script>

</asp:Content>

