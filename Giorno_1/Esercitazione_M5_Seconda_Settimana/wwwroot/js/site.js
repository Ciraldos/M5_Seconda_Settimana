
<script>
    $(document).ready(function () {
        $('#searchButton').click(function () {
            var cf = $('#cf').val();
            $.ajax({
                url: '/api/PrenotazioniControllerAPI',
                type: 'GET',
                data: { cf: cf },
                success: function (data) {
                    var container = $('#prenotazioniContainer');
                    container.empty();
                    if (data.length === 0) {
                        container.append('<p>Nessuna prenotazione trovata.</p>');
                    } else {
                        var table = $('<table>').append('<tr><th>IdPrenotazione</th><th>NumProgressivo</th><th>Anno</th><th>SoggiornoDal</th><th>SoggiornoAl</th><th>Caparra</th><th>Tariffa</th><th>PensioneCompleta</th><th>IdCliente</th><th>IdCamera</th></tr>');
                        data.forEach(function (prenotazione) {
                            var row = $('<tr>');
                            row.append('<td>' + prenotazione.idPrenotazione + '</td>');
                            row.append('<td>' + prenotazione.numProgressivo + '</td>');
                            row.append('<td>' + prenotazione.anno + '</td>');
                            row.append('<td>' + prenotazione.soggiornoDal + '</td>');
                            row.append('<td>' + prenotazione.soggiornoAl + '</td>');
                            row.append('<td>' + prenotazione.caparra + '</td>');
                            row.append('<td>' + prenotazione.tariffa + '</td>');
                            row.append('<td>' + prenotazione.pensioneCompleta + '</td>');
                            row.append('<td>' + prenotazione.idCliente + '</td>');
                            row.append('<td>' + prenotazione.idCamera + '</td>');
                            table.append(row);
                        });
                        container.append(table);
                    }
                },
                error: function (xhr, status, error) {
                    $('#prenotazioniContainer').empty().append('<p>Errore durante la ricerca delle prenotazioni: ' + error + '</p>');
                }
            });
        });
        });
</script>