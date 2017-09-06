$(document).ready(function () {

    setInterval(function () {
        actualizar();
    }, 60000);

    actualizar();
    function actualizar(){
        $("#listadoCursos").html("");

        $.post("/Cursos/ListadoCursosAjax", function (data) {

            for (var i = 0; i < data.length; i++) {
                if (data[i].cursos.length > 0) {
                    $("#listadoCursos").append(codigoPanel(data[i]));
                }
            }
        });

    }
    function codigoPanel(curso) {
        var nombreCategoria = curso.Nombre;
        var cursos = curso.cursos;

        var html = "<div class='panel panel-default'>";
        html += "<div class='panel-heading'>";
        html += "<h2>" + nombreCategoria +"</h2>";
        html += "</div>";
        html += "<div class='panel-body'>";
        html += "<div class='row text-center'>";
        for (var i = 0; i < cursos.length; i++) {
            html += codigoCurso(cursos[i].Id, cursos[i].Portada, cursos[i].Titulo);
        }
        html += "</div></div></div>";
        return html;
    }

    function codigoCurso(idEnlace,rutaImagen,titulo) {
        var html = " <div class='col-sm-6 col-md-2 col-md-offset-1'>";
        rutaImagen = (rutaImagen != null && rutaImagen != "null") ? rutaImagen: "/Content/img/sinimagen.png"
        html += "<a href='/Cursos/Ver/" + idEnlace + "' class='thumbnail'><img src='" + rutaImagen + "' alt='" + titulo + "' class='imagenCurso'></a>";
        html += "<p>" + titulo + "</p>";
        html += "</div>";
        return html;
    }


});