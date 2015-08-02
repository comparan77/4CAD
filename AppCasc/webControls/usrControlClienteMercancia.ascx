<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="usrControlClienteMercancia.ascx.cs" Inherits="AppCasc.webControls.usrControlClienteMercancia" %>
<div id="ctrlClienteMercancia" title="Nueva Mercancia" class="divForm hidden">
    <div>
        <label>Clase:</label>
        <select id="txt_clase">
            <option value="F&H">F&H</option>
            <option value="COS">COS</option>
            <option value="COM">COM</option>
        </select>
    </div>
    <div>
        <label>Negocio:</label>
        <select id="txt_negocio" class="txtSmall"><%=optionNegocio %></select>
    </div>
    <div>
        <label>C&oacute;digo:</label>
        <input type="text" id="txt_codigo" class="requeridoCM" />
    </div>
    <div>
        <label>Nombre:</label>
        <input type="text" id="txt_nombre" class="requeridoCM" />
    </div>
    <div>
        <label>Unidad:</label>
        <select id="txt_unidad" class="txtSmall">
            <option value="PZ">PZ</option>
            <option value="CJ">CJ</option>
        </select>
    </div>

</div>