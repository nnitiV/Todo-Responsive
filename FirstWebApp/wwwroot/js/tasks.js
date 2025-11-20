$('.form-button').on('click', async function (e) {
    e.preventDefault();
    var button = $(this);
    var handler = button.attr("formaction");
    var form = button.closest('form');
    var taskId = form.find('#Id').val();
    var token = form.find('input[name="__RequestVerificationToken"]').val();
    var formData = new FormData();
        formData.append('Id', taskId);
        formData.append('__RequestVerificationToken', token);
    if (handler === "/Tasks?handler=Update") {
        var title = form.find('#Title').val();
        formData.append('Title', title);
    }

    const response = await fetch(handler, {
        method: 'POST',
        body: formData,
    });

    if (response.ok) {
        const result = await response.json();
        if (result.success && handler === "/Tasks?handler=Delete") {
            form.closest("li").remove();
        } else if (handler === "/Tasks?handler=Update") {
            form.find(".Title").val(result.task.title);
        } else if (handler === "/Tasks?handler=Complete") {
            form.find(".IsCompleted").text(result.task.isCompleted ? "Completed" : "Not Completed");
        }
    } else {
        console.log("Could not connect to server.");
    }
});