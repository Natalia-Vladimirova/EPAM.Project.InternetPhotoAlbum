$(function () {
    $('#liveSearch').keyup(function () {
        var search = $('#liveSearch').val();
        var userName = $('#userName').val();
        $.ajax({
            type: 'POST',
            url: '/Photo/SearchPhotosAjax',
            data: { 'photoName': search, 'userName': userName },
            cache: false,
            success: function (response) {
                $('#photosList').html(response);
                $('#bigPhoto').html('<center><img class="big-photo" src="/Content/no-selected-photo.png" /></center>');
            }
        });
    });
});