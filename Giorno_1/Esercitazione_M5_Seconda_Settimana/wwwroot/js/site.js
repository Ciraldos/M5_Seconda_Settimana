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
            let container = $("#prenotazioniContainer");
            container.empty();
            $(data).each((_, prenotazione) => {
                let card = $(`<div class="card mt-3">
            <div class="card-body">
            <h5 class="card-title">Prenotazione ID: ${prenotazione.idPrenotazione}</h5>
            <p class="card-text">NumProgressivo: ${prenotazione.numProgressivo}</p>
            <p class="card-text">Anno: ${prenotazione.anno}</p>
            <p class="card-text">SoggiornoDal: ${prenotazione.soggiornoDal}</p>
            <p class="card-text">SoggiornoAl: ${prenotazione.soggiornoAl}</p>
            <p class="card-text">Caparra: ${prenotazione.caparra}</p>
            <p class="card-text">Tariffa: ${prenotazione.tariffa}</p>
            <p class="card-text">PensioneCompleta: ${prenotazione.pensioneCompleta}</p>
            <p class="card-text">IdCliente: ${prenotazione.idCliente}</p>
            <p class="card-text">IdCamera: ${prenotazione.idCamera}</p>
            </div>
            </div>`);
                container.append(card);
            });
        },
        error: (xhr, status, error) => {
            let container = $("#prenotazioniContainer");
            container.empty();
            container.append('<li>Errore durante la ricerca delle prenotazioni: ' + error + '</li>');
        }
    });
}

function getPrenotazioniByPensione() {
    let pensione = $("#pensione").val();
    $.ajax({
        url: `${basePath}/ByPensione?p=${pensione}`,
        method: 'get',
        success: (data) => {
            let container = $("#prenotazioniContainer");
            container.empty();
            $(data).each((_, prenotazione) => {
                let card = $(`<div class="card mt-3">
            <div class="card-body">
            <h5 class="card-title">Prenotazione ID: ${prenotazione.idPrenotazione}</h5>
            <p class="card-text">NumProgressivo: ${prenotazione.numProgressivo}</p>
            <p class="card-text">Anno: ${prenotazione.anno}</p>
            <p class="card-text">SoggiornoDal: ${prenotazione.soggiornoDal}</p>
            <p class="card-text">SoggiornoAl: ${prenotazione.soggiornoAl}</p>
            <p class="card-text">Caparra: ${prenotazione.caparra}</p>
            <p class="card-text">Tariffa: ${prenotazione.tariffa}</p>
            <p class="card-text">PensioneCompleta: ${prenotazione.pensioneCompleta}</p>
            <p class="card-text">IdCliente: ${prenotazione.idCliente}</p>
            <p class="card-text">IdCamera: ${prenotazione.idCamera}</p>
            </div>
            </div>`);
                container.append(card);
            });
        },
        error: (xhr, status, error) => {
            let ul = $("#prenotazioniContainer");
            container.empty();
            container.append('<li>Errore durante la ricerca delle prenotazioni: ' + error + '</li>');
        }
    });
}
