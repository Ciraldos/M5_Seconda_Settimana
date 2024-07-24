let basePath = '/api/PrenotazioniControllerAPI';

$(() => {
    $("#searchButton").on('click', () => {
        getPrenotazioni();
    });
});

function getPrenotazioni() {
    let cf = $("#cf").val();
    $.ajax({
        url: `${basePath}`,
        method: 'get',
        success: (data) => {
            let ul = $("#prenotazioniContainer ul");
            ul.empty();
            $(data).each((_, prenotazione) => {
                let textSpan = $('<span>').text(`Prenotazione ID: ${prenotazione.idPrenotazione}, NumProgressivo: ${prenotazione.numProgressivo}, Anno: ${prenotazione.anno}, SoggiornoDal: ${prenotazione.soggiornoDal}, SoggiornoAl: ${prenotazione.soggiornoAl}, Caparra: ${prenotazione.caparra}, Tariffa: ${prenotazione.tariffa}, PensioneCompleta: ${prenotazione.pensioneCompleta}, IdCliente: ${prenotazione.idCliente}, IdCamera: ${prenotazione.idCamera}`);
                let li = $('<li>');
                textSpan.appendTo(li);
                li.appendTo(ul);
            });
        },
        error: (xhr, status, error) => {
            let ul = $("#prenotazioniContainer ul");
            ul.empty();
            ul.append('<li>Errore durante la ricerca delle prenotazioni: ' + error + '</li>');
        }
    });
}