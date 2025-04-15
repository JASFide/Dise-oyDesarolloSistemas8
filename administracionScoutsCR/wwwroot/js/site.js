// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
        function exportarAPdf() {
            const botones = document.getElementById('botonesExportar');
            botones.style.display = 'none';
            const acciones = document.querySelectorAll('.col-acciones');
            acciones.forEach(e => e.style.display = 'none');
            const contenido = document.getElementById('contenidoExportar');
            const opciones = {
                margin: 0.5,
                filename: 'ContactosEmergencia.pdf',
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: 2 },
                jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
            };
            html2pdf().set(opciones).from(contenido).save().then(() => {
                botones.style.display = 'flex';
                acciones.forEach(e => e.style.display = '');
            });
        }
