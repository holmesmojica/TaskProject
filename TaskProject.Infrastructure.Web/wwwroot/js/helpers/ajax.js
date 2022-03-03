
const requestMethods =
{
    DELETE: 'DELETE',
    GET: 'GET',
    POST: 'POST',
    PUT: 'PUT'
};


const httpStatusCodes =
{
    SUCCESS: 200,
    CREATED: 201,
    NOT_FOUND: 401
};


const requestAsync = async (ajaxurl, args = {}, requestMethod = requestMethods.GET, loading = true) =>
{
    if (loading)
    {
        showLoading();
    }

    let result;

    try
    {
        result = await $.ajax({
            url: ajaxurl,
            type: requestMethod,
            data: args,
            dataType: "json",
            //accepts: "application/json",
            contentType: "application/x-www-form-urlencoded"
        });

        if (loading)
            hideLoading();

        showQueryResultNotification(result.status);
        return result;
    }
    catch (error)
    {
        if (loading)
            hideLoading();

        console.log('Process error', error.message);
        return null;
    }
}


const showQueryResultNotification = (queryStatus) =>
{
    switch (queryStatus)
    {
        case httpStatusCodes.SUCCESS:
            showNotification('Process Successfully', notificationTypes.SUCCESS);
        break;

        case httpStatusCodes.CREATED:
            showNotification('Creation Process Successfull', notificationTypes.SUCCESS);
        break;

        case httpStatusCodes.NOT_FOUND:
            showNotification('No data found', notificationTypes.WARNING);
        break;
    }
};