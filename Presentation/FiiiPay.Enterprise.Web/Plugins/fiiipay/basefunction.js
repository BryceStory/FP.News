jQuery.extend({
    "openmodalpage": function (pageid, url, para, title, widthstr, onclose) {
        var excistDiv = $("#bootstrap-modal-" + pageid);
        if (excistDiv.length > 0) {
            return;
        }
        var modalfadeclass;
        var modaldialogclass;
        switch (widthstr) {
            case "big":
                modalfadeclass = "bs-example-modal-lg";
                modaldialogclass = "modal-lg";
                break;
            case "small":
                modalfadeclass = "bs-example-modal-sm";
                modaldialogclass = "modal-sm";
                break;
            case "normal":
            default:
                modalfadeclass = "";
                modaldialogclass = "";
                break;
        }
        var modelCount = $(".modal-backdrop").length;
        $.post(url, para, function (html) {
            modaldiv = $('<div class="modal ' + modalfadeclass + '" id="bootstrap-modal-' + pageid + '" tabindex="-1" role="dialog" aria-labelledby="bootstrap-modal-label"></div>');
            var modalhtml = '<div class="modal-dialog ' + modaldialogclass + '" role="document">';
            modalhtml += '<div class="modal-content">';
            modalhtml += '<div class="modal-header">';
            modalhtml += '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
            modalhtml += '<h4 class="modal-title" id="bootstrap-modal-label">' + title + '</h4></div>';
            modalhtml += '<div class="modal-body" style="min-height:200px;">';
            modalhtml += html;
            modalhtml += '</div></div></div>';
            modaldiv.append(modalhtml);
            $("body").append(modaldiv);
            modaldiv.on('hide.bs.modal', function (e) {
                if (onclose) {
                    var arg = modaldiv.data("closedata");
                    onclose.call(this, arg);
                }
                $(this).remove();
            });
            modaldiv.modal({ backdrop: 'static', keyboard: false });
        });
    },
    "closemodalpage": function (pageid, closedata) {
        var modaldiv = $("#bootstrap-modal-" + pageid);
        if (modaldiv.length <= 0) {
            return;
        }
        modaldiv.data("closedata", closedata);
        modaldiv.modal('hide');
    },
    "openloadding": function () {
        var loadDiv = $('<div class="f-loadding"></div>');
        var loadimg = '<div class="f-loadding-img"><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span></div>';
        loadDiv.append(loadimg);
        $("body").append(loadDiv);
    },
    "closeloadding": function () {
        var loadDiv = $(".f-loadding");
        loadDiv.remove();
    },
    "swconfirm": function (text, onclose) {
        swal({
            title: "",
            text: text,
            showCancelButton: "true",
            confirmButtonText: "Confirm",
            cancelButtonText: "Cancel",
            type: "warning",
            closeOnConfirm: false
        }, onclose);
    }
});
var BaseFunction = {
    GetLanguage :  function() {
        return $("#f-frame-language").data("lang");
    },
    getresource: function (resourcetype, resourcekey) {
        var lang = BaseFunction.GetLanguage();
        var typevalue = 'fiiienterpriseresources.' + resourcetype + "." + resourcekey + "." + lang;
        var resourcevalue = eval(typevalue);
        if (!resourcevalue) {
            return;
        }
        if (arguments.length > 2) {
            for (var i = 2; i < arguments.length; i++) {
                resourcevalue = resourcevalue.replace('{' + (i - 2) + '}', arguments[i]);
            }
        }
        return resourcevalue;
    },
    SendEmailCode: function (btn,url,email,onfaild) {
        var $btn = $(btn);
        if ($btn.hasClass("codesent")) {
            return false;
        }
        if (!email)
            return false;
        $btn.addClass("codesent");
        var postdata = JSON.stringify({ Email: email });
        $.ajax({
            url: url,
            data: postdata,
            contentType: 'application/json',
            dataType: 'json',
            success: function (context) {
                if (context.Status) {
                    var timeremain = 60;
                    $btn.text(BaseFunction.getresource('generalresource', 'resendcode', timeremain));
                    var timer = setInterval(sentcode, 1000);
                }
                else {
                    $btn.removeClass("codesent");
                    if (onfaild) {
                        onfaild.call($btn, context);
                    }
                }
                function sentcode() {
                    timeremain--;
                    $btn.text(BaseFunction.getresource('generalresource', 'resendcode', timeremain));
                    if (timeremain <= 0) {
                        clearInterval(timer);
                        $btn.removeClass("codesent");
                        $btn.text(BaseFunction.getresource('generalresource', 'sendcode'));
                    }
                }
            }
        });
    }
};