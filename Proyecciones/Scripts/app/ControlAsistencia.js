var tabla = $('#example').DataTable({
    columns:
        [
            { title: "#" },
            { title: "Docente" },
            { title: "Horas a Trabajar" },
            { title: "Horas Trabajadas" },
            { title: "Turno" },
            { title: '' }
        ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
    }
});
$("#cargando").show();
$("#contenido").hide();
$(document).ready(function () {
    GetOfertaActual();
});
function GetOfertas(activo) {
    $.getJSON('@Url.Action("Get","Oferta")', (data) => {
        $("#ofertas").html("");
        $.each(data, function (i, val) {
            if (activo == val.Id)
                var boton = '<button id="Oferta' + val.Id + '" class="list-group-item active" onclick="SeleccionarOferta(' + val.Id + ')">' + val.Descripcion + '</button>';
            else
                var boton = '<button  id="Oferta' + val.Id + '" class="list-group-item" onclick="SeleccionarOferta(' + val.Id + ')">' + val.Descripcion + '</button>';
            $("#ofertas").append(boton);
        });
        $("#cargando").hide();
        $("#contenido").show();
    })
}
function GetOfertaActual() {
    $.getJSON('@Url.Action("GetActual","Oferta")', (data) => {
        GetOfertas(data.Id);
        GetSolicitudes(data.Id);
    })
}
function GetOfertaActual() {
    $.getJSON('@Url.Action("GetActual","Oferta")', (data) => {
        GetOfertas(data.Id);
        //GetSolicitudes(data.Id);
    })
}
function SeleccionarOferta(id) {
    $(".list-group-item").removeClass('active');
    $("#Oferta" + id).addClass('active');
    //GetSolicitudes(id);
}