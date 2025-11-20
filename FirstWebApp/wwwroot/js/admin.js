console.log("Admin JS loaded."); 
$('.admin-action-btn').on('click', async function (e) {
    e.preventDefault();
    var button = $(this);
    var form = button.closest('form');
    var row = button.closest('tr');
    var handlerUrl = button.attr('formaction');

    var formData = new FormData(form[0]);

    try {
        const response = await fetch(handlerUrl, {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            const result = await response.json();
            console.log(result);
            if (result.success) {
                if (result.action === "delete") {
                    form.closest('tr').remove();
                    row.fadeOut(300, function () { $(this).remove(); });
                }
                else if (result.action === "update") {
                    var badgeBtn = row.find('.dropdown-toggle');

                    if (result.isCompleted) {
                        badgeBtn.removeClass('btn-warning').addClass('btn-success').text('Completed');
                    } else {
                        badgeBtn.removeClass('btn-success').addClass('btn-warning').text('Pending');
                    }
                }
            } else {
                console.error("Action failed.");
            }
        }
    } catch (error) {
        console.error('Error:', error);
        console.error("Could not connect to server.");
    }
});
