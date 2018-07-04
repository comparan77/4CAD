﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCatalogos.aspx.cs" Inherits="logistica.pages.catalog.frmCatalogos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Catálogos</h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-3 col-md-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-building-o fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><%= bodegaCantidad.ToString() %></div>
                        <div>Bodegas</div>
                    </div>
                </div>
            </div>
            <a href="frmBodega.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-green">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-columns fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><%= cortinaCantidad.ToString() %></div>
                        <div>Cortinas</div>
                    </div>
                </div>
            </div>
            <a href="frmCortina.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-yellow">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="glyphicon glyphicon-user fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><%= clienteCantidad.ToString() %></div>
                        <div>Clientes</div>
                    </div>
                </div>
            </div>
            <a href="frmCliente.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-brown">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-cube fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><%= mercanciaCantidad.ToString() %></div>
                        <div>Mercanc&iacute;a</div>
                    </div>
                </div>
            </div>
            <a href="frmMercancia.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-red">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-briefcase fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><%= servicioCantidad.ToString() %></div>
                        <div>Servicios</div>
                    </div>
                </div>
            </div>
            <a href="frmServicio.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-golden">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-dollar fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge">&nbsp;</div>
                        <div>Tarifas</div>
                    </div>
                </div>
            </div>
            <a href="frmTarifa.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-gray">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-location-arrow fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><% = destinatarioCantidad.ToString() %></div>
                        <div>Destinos</div>
                    </div>
                </div>
            </div>
            <a href="frmDestino.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-aliceblue">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-truck fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><% = transportistaCantidad.ToString() %></div>
                        <div>Transportistas</div>
                    </div>
                </div>
            </div>
            <a href="frmTransporte.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

    <div class="col-lg-3 col-md-6">
        <div class="panel panel-darkseagreen">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3">
                        <i class="fa fa-truck fa-5x"></i>
                    </div>
                    <div class="col-xs-9 text-right">
                        <div class="huge"><% = vendorCantidad.ToString() %></div>
                        <div>Vendor</div>
                    </div>
                </div>
            </div>
            <a href="frmVendor.aspx">
                <div class="panel-footer">
                    <span class="pull-left">Acceder</span>
                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                    <div class="clearfix"></div>
                </div>
            </a>
        </div>
    </div>

</div>
</asp:Content>
