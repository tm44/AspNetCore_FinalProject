// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var PlaceHolderHere = $("#PlaceHolderHere");

    $("button[data-toggle='ajax-modal']").click(function (event) {
        var url = $(this).data("url") + "/" + $(this).data("personid");
        $.get(url).done(function (data) {
            PlaceHolderHere.html(data);
            PlaceHolderHere.html(data).find(".modal").modal("show");
        })
    })

    PlaceHolderHere.on("click", "[data-save='modal']", function (event) {
        var form = $(this).parents(".modal").find("form");
        var personId = form.attr("data-personid");
        debugger;
        var note = {
            Text: form[0].elements[0].value,
            PersonId: personId
        }
        //var sendData =  form.serialize();
        var sendData = JSON.stringify(note);
        $.ajax({
            url: "/note/" + form.attr("action"),
            data: sendData,
            type: "POST",
            contentType: "application/json",
            datatype: "json",
            success: function (data) {
                var actionUrl = "/note/" + form.attr("action");
            }
        })
    })
})