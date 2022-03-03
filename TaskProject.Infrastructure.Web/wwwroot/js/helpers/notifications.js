
const notificationTypes =
{
    SUCCESS: 'success',
    WARNING: 'warning',
    ERROR: 'error'
};


const showNotification = function (message, notificationType)
{
    notificationType = notificationType || 'success';

    toastr.clear();
    toastr.options = getNotificationOptions();
    toastr[notificationType](message, notificationType);
};


let getNotificationOptions = () =>
{
    let options =
    {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "7000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    return options;
};


const showAlert = async (title, text, icon) =>
{
    return await Swal.fire
    ({
        title: title,
        html: text,
        icon: icon,
        showCancelButton: true,
        confirmButtonText: 'Ok',
        confirmButtonColor: '#3085d6',
        cancelButtonText: 'Cancel',
        cancelButtonColor: '#757575',
        reverseButtons: true
    });
};