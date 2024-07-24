let basePath = '/api/contacts'

$(() => {
    getContacts()

    $("#insert").on('click', () => {
        let name = $("#name").val()
        let phone = $("#phone").val()
        let contact = { "name": `${name}`, "phone": `${phone}` }
        $.ajax({
            url: `${basePath}`,
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify(contact),
            success: () => getContacts()
        })
    })
})
function getContacts() {
    $.ajax({
        url: `${basePath}`,
        method: 'get',
        success: (data) => { 
            let ul = $("#contact-list ul")
            ul.empty()
            $(data).each((_, contact) => {
                let textSpan = $('<span>').text(`${contact.name} - ${contact.phone}`)
                let deleteBtn = $("<button type='button'>").attr('data-id', contact.id).text('Elimina')
                let li = $('<li>')
                textSpan.appendTo(li)
                deleteBtn.appendTo(li)
                li.appendTo(ul)

                $(deleteBtn).on('click', () => {
                    $.ajax({
                        url: `${basePath}/${contact.id}`,
                        method: 'delete',
                        success: getContacts()
                    })
                })
            })
        }
    })
}