const MessageType = {
    Create: 'Create',
    Edit: 'Edit',
    Delete: 'Delete',
    Validation: 'Validation',
    Error: 'Error',
    Info: 'Info'
};
function ShowMessage(message, messageType) {
    var color = '';
    var icon = '';
    var title = '';
    if (messageType == MessageType.Create) {
        color = '#2e3e81';
        icon = 'success';
        title = 'Success';
    }
    else if (messageType == MessageType.Edit) {
        color = '#2e3e81';
        icon = 'success';
        title = 'Success';
    }
    else if (messageType == MessageType.Delete) {
        color = '#2e3e81';
        icon = 'success';
        title = 'Success';
    }
    else if (messageType == MessageType.Error) {
        color = 'rgba(128,0,0,0.9)';
        icon = 'error';
        title = 'Error';
    }
    else if (messageType == MessageType.Validation) {
        color = 'rgba(10, 87, 201)';
        icon = 'warning';
        title = 'Alert';
    }
    else if (messageType == MessageType.Info) {
        color = '#2e3e81';
        icon = 'info';
        title = 'Info';
    }

    Swal.fire({
        icon: icon,
        title: title,
        confirmButtonColor: color,
        html: message,
        showCloseButton: true,
    });

}