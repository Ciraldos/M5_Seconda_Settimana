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
                let card = $(`<div class="col-md-4 mt-3">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        Dettagli Prenotazione
                    </div>
                    <div class="card-body card-body-custom">
                        <p class="card-text"><i class="fas fa-id-badge icon"></i><span>ID Prenotazione:</span> ${prenotazione.idPrenotazione}</p>
                        <p class="card-text"><i class="fas fa-list-ol icon"></i><span>Num Progressivo:</span> ${prenotazione.numProgressivo}</p>
                        <p class="card-text"><i class="fas fa-calendar-alt icon"></i><span>Anno:</span> ${prenotazione.anno}</p>
                        <p class="card-text"><i class="fas fa-calendar-day icon"></i><span>Soggiorno Dal:</span> ${prenotazione.soggiornoDal}</p>
                        <p class="card-text"><i class="fas fa-calendar-check icon"></i><span>Soggiorno Al:</span> ${prenotazione.soggiornoAl}</p>
                        <p class="card-text"><i class="fas fa-money-check-alt icon"></i><span>Caparra:</span> €${prenotazione.caparra}</p>
                        <p class="card-text"><i class="fas fa-dollar-sign icon"></i><span>Tariffa:</span> €${prenotazione.tariffa}</p>
                        <p class="card-text"><i class="fas fa-bed icon"></i><span>Pensione Completa:</span> ${prenotazione.pensioneCompleta}</p>
                        <p class="card-text"><i class="fas fa-user icon"></i><span>Id Cliente:</span> ${prenotazione.idCliente}</p>
                        <p class="card-text"><i class="fas fa-door-open icon"></i><span>Id Camera:</span> ${prenotazione.idCamera}</p>
                    </div>
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
            let x = data.length
            let num = $(`<h1 class="fs-2 text-center myPurple mt-3 fw-bold">Numero prenotazioni trovate: ${x}</h1>`);
            container.append(num);
            $(data).each((_, prenotazione) => {
                let card = $(`<div class="col-md-4 mt-3">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        Dettagli Prenotazione
                    </div>
                    <div class="card-body card-body-custom">
                        <p class="card-text"><i class="fas fa-id-badge icon"></i><span>ID Prenotazione:</span> ${prenotazione.idPrenotazione}</p>
                        <p class="card-text"><i class="fas fa-list-ol icon"></i><span>Num Progressivo:</span> ${prenotazione.numProgressivo}</p>
                        <p class="card-text"><i class="fas fa-calendar-alt icon"></i><span>Anno:</span> ${prenotazione.anno}</p>
                        <p class="card-text"><i class="fas fa-calendar-day icon"></i><span>Soggiorno Dal:</span> ${prenotazione.soggiornoDal}</p>
                        <p class="card-text"><i class="fas fa-calendar-check icon"></i><span>Soggiorno Al:</span> ${prenotazione.soggiornoAl}</p>
                        <p class="card-text"><i class="fas fa-money-check-alt icon"></i><span>Caparra:</span> €${prenotazione.caparra}</p>
                        <p class="card-text"><i class="fas fa-dollar-sign icon"></i><span>Tariffa:</span> €${prenotazione.tariffa}</p>
                        <p class="card-text"><i class="fas fa-bed icon"></i><span>Pensione Completa:</span> ${prenotazione.pensioneCompleta}</p>
                        <p class="card-text"><i class="fas fa-user icon"></i><span>Id Cliente:</span> ${prenotazione.idCliente}</p>
                        <p class="card-text"><i class="fas fa-door-open icon"></i><span>Id Camera:</span> ${prenotazione.idCamera}</p>
                    </div>
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
