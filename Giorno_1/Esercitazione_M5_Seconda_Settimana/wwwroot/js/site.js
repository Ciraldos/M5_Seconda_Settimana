let basePath = '/api/PrenotazioniControllerAPI';

$(() => {
    $("#searchButton").on('click', () => {
        getPrenotazioniByCf();
    });

    $("#searchButtonP").on('click', () => {
        getPrenotazioniByPensione();
    });
});

function getPrenotazioniByCf() {
    let cf = $("#cf").val();
    $.ajax({
        url: `${basePath}/ByCf?cf=${cf}`,
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

function getPrenotazioniByPensione() {
    let pensione = $("#pensione").val();
    $.ajax({
        url: `${basePath}/ByPensione?p=${pensione}`,
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
