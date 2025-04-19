function exportarAPdf(idContenido = "contenidoExportar", nombreArchivo = "reporte") {
    const contenidoOriginal = document.getElementById(idContenido);
    if (!contenidoOriginal) {
        console.error("No se encontró el contenedor a exportar.");
        return;
    }

    const tablaOriginal = contenidoOriginal.querySelector("table");
    if (!tablaOriginal) {
        console.error("No se encontró ninguna tabla dentro del contenedor.");
        return;
    }

    // Clonar y limpiar
    const tabla = tablaOriginal.cloneNode(true);
    tabla.querySelectorAll("thead tr th:last-child").forEach(th => th.remove());
    tabla.querySelectorAll("tbody tr").forEach(tr => tr.removeChild(tr.lastElementChild));

    tabla.removeAttribute("class");
    tabla.querySelectorAll("*").forEach(el => {
        el.removeAttribute("class");
        el.removeAttribute("style");
    });

    tabla.style.width = "100%";
    tabla.style.borderCollapse = "collapse";
    tabla.querySelectorAll("th").forEach(th => {
        th.style.border = "1px solid black";
        th.style.padding = "8px";
        th.style.backgroundColor = "#ffffff";
        th.style.color = "#000000";
        th.style.fontWeight = "bold";
        th.style.textAlign = "left";
    });
    tabla.querySelectorAll("td").forEach(td => {
        td.style.border = "1px solid black";
        td.style.padding = "8px";
        td.style.color = "#000000";
        td.style.backgroundColor = "#ffffff";
    });

    const now = new Date();
    const fechaHora = now.toLocaleString("es-CR", {
        year: 'numeric', month: 'long', day: 'numeric',
        hour: '2-digit', minute: '2-digit'
    });

    const encabezado = document.createElement("div");
    encabezado.style.textAlign = "center";
    encabezado.style.marginBottom = "20px";
    encabezado.innerHTML = `
        <h2 style="color: black; margin: 0;">Reporte</h2>
        <p style="font-size: 12px; color: black;">Generado el ${fechaHora}</p>
    `;

    const contenedorPDF = document.createElement("div");
    contenedorPDF.style.backgroundColor = "#ffffff";
    contenedorPDF.style.color = "#000000";
    contenedorPDF.style.padding = "20px";
    contenedorPDF.style.fontFamily = "Arial, sans-serif";
    contenedorPDF.appendChild(encabezado);
    contenedorPDF.appendChild(tabla);

    html2pdf().set({
        margin: 0.5,
        filename: `${nombreArchivo}_${now.toISOString().slice(0, 10)}.pdf`,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2, backgroundColor: "#ffffff" },
        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    }).from(contenedorPDF).save();
}
