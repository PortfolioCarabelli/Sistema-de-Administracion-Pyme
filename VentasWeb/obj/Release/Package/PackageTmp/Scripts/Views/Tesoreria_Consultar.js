



var tabladata
$(document).ready(function () {

    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: true,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);


    $("#txtFechaInicio").datepicker();
    $("#txtFechaFin").datepicker();

});
function productoSelect(compras, ventas) {
    ResultChart = [{
        name: 'Ventas',
        y: ventas,
        sliced: true,
        selected: true
    }, {
        name: 'Compras',
        y: compras,
        sliced: true,
        selected: true
    }],
      
    DibujarGrafico(ResultChart);
}

function OnErrorCall_(response) {
    alert("Whoops something went wrong!");
}

function buscar() {
    
    if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
        swal("Mensaje", "Debe ingresar fechas", "warning")
        return;
    }
    if ($("#txtFechaInicio").val().trim() == 0 || $("#txtFechaFin").val().trim() == 0) {
        swal("Mensaje", "Debe ingresar fechas", "warning")
        $("#txtFechaFin").val("");
        $("#txtFechaInicio").val("");
        return;
    }
    $("#Descripcion").removeClass("invisible");
    $("#DescripcionC").removeClass("invisible");
   
    $("#tbdata").removeClass("invisible");
    $("#cboDetalle").removeClass("invisible");
    $("#btnBuscarDetalle").removeClass("invisible");
    $("#lblDetalle").removeClass("invisible");
    $("#tbdata").dataTable().fnDestroy();
    BuscarDetalle()
    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url._ObtenerDatos + "?fechainicio=" + $("#txtFechaInicio").val().trim() + " &fechafin=" + $("#txtFechaFin").val().trim(),
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "Ventas", render: function (data) {
                    return data
                }
            },
            {
                "data": "Compras", render: function (data) {
                    return data
                }
            },
        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });

    jQuery.ajax({
        url: $.MisUrls.url._ObtenerDatos + "?fechainicio=" + $("#txtFechaInicio").val().trim() + " &fechafin=" + $("#txtFechaFin").val().trim(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $.each(data.data, function (i, item) {
                compras = (item.Compras)
                ventas = (item.Ventas)
            })
            productoSelect(compras, ventas)
        },
        error: function (error) {
            console.log(error)
        },
    });
}

function ObtenerFecha() {

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + d.getFullYear();

    return output;
}

function BuscarDetalle() {
    $("#lbl1").removeClass("invisible");
    $("#lbl2").removeClass("invisible");
  
        $("#tbCompras").removeClass("invisible");
        $("#tbCompras").dataTable().fnDestroy();
        tablaCompras = $('#tbCompras').DataTable({
            "ajax": {
                "url": $.MisUrls.url._ObtenerComprasTesoreria + "?fechainicio=" + $("#txtFechaInicio").val().trim() + "&fechafin=" + $("#txtFechaFin").val().trim() + "&idproveedor=0&idtienda=0",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                {
                    "data": "IdCompra", render: function (data) {
                        return "<button class='btn btn-success btn-sm ml-2' type='button' onclick='Imprimir(" + data + ")'><i class='far fa-clipboard'></i> Ver</button>"
                    }
                    
                },
                { "data": "NumeroCompra" },
                {
                    "data": "oProveedor", render: function (data) {
                        return data.RazonSocial
                    }
                },
                {
                    "data": "oTienda", render: function (data) {
                        return data.Nombre
                    }
                },
                { "data": "FechaCompra" },
                {
                    "data": "TotalCosto", render: function (data) {

                        return "S./ " + (data).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
                    }
                },


            ],
            "language": {
                "url": $.MisUrls.url.Url_datatable_spanish
            },
            responsive: true
        });
    
        
    
        $("#tbVentas").removeClass("invisible");
        $("#tbVentas").dataTable().fnDestroy();
        tablaVenta = $('#tbVentas').DataTable({
            "ajax": {
                "url": $.MisUrls.url._ObtenerVentasTesoreria + "?codigo=&fechainicio=" + $("#txtFechaInicio").val().trim() + "&fechafin=" + $("#txtFechaFin").val().trim() + "&numerodocumento=&nombres=",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                {
                    "data": "IdVenta", render: function (data) {
                        return "<button class='btn btn-success btn-sm ml-2' type='button' onclick='Imprimir(" + data + ")'><i class='far fa-clipboard'></i> Ver</button>"
                    }
                    
                },
                { "data": "TipoDocumento" },
                { "data": "Codigo" },
                { "data": "FechaRegistro" },
                {
                    "data": "oCliente", render: function (data) {
                        return data.NumeroDocumento
                    }
                },
                {
                    "data": "oCliente", render: function (data) {
                        return data.Nombre
                    }
                },
                {
                    "data": "TotalCosto", render: function (data) {

                        return "S./ " + (data).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
                    }
                },
            ],
            "language": {
                "url": $.MisUrls.url.Url_datatable_spanish
            },
            responsive: true
        });
    
}




function DibujarGrafico(data) {
    $('#container').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',

        },
        title: { text: 'Balance' },
        tooltip: { pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>' },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                },
            },
        },

        series: [{
            name: 'Porcentaje',
            colorByPoint: true,
            data: data,
        }]
    });

    $('#containerColumns').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Porcentajes X Mes'
        },
        
        xAxis: {
            categories: [
                'Ene',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dic'
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Montos Totales'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} %b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: 'Compras',
            data: [49.9, 71.5, 50.4, 20.2, 60.0, 76.0, 35.6, 48.5, 16.4, 94.1, 95.6, 54.4]

        }, {
            name: 'Ventas',
            data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 60.0, 90.3, 91.2, 83.5, 66.6, 92.3]

        }]
    });
}