(function ($) {
    var _ajax = $.ajax;
    $.ajax = function (opt) {
        var hideloadding = opt.hideloadding;
        var lang = BaseFunction.GetLanguage();
        var errorfun = opt.error;
        opt.error = null;
        opt.url = '/' + lang + opt.url;
        var _opt = $.extend(opt, {
            beforeSend: function (xhr) {
                if (!hideloadding) {
                    $.openloadding();
                }
            },
            error: function (xhr, s) {
                if (errorfun) {
                    errorfun.call(null, xhr, s);
                }
                var sessionstatus = xhr.getResponseHeader('sessionstatus');
                if (sessionstatus === 'timeout') {
                    window.top.location.href = '/login';
                }
            },
            complete: function (xhr, ts) {
                if (!hideloadding) {
                    $.closeloadding();
                }
            },
            headers: {},
            type: 'post'
        });
        return _ajax(_opt);
    };
})(jQuery);
(function ($) {
    var _swal = swal;
    swal = function (opt, callback) {
        var _opt = $.extend(opt, { confirmButtonText: BaseFunction.getresource("generalresource", "btnconfirm") });
        return _swal(_opt, callback);
    };
})(jQuery);
(function ($) {
    var defaults = {
        showDropdowns: true,
        autoApply: true,
        format: 'YYYY-MM-DD HH:mm'
    };
    var languageresource = {
        applyLabel: { en: "Apply", cn: "确定" },
        cancelLabel: { en: "Cancel", cn: "取消" },
        fromLabel: { en: "From", cn: "" },
        toLabel: { en: "to", cn: "至" },
        customRangeLabel: { en: "Custom", cn: "自定义" },
        daysOfWeek: { en: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"], cn: ["日", "一", "二", "三", "四", "五", "六"] },
        monthNames: {
            en: ["January","February","March","April","May","June","July","August","September","October","November","December"],
            cn: ["1月","2月","3月","4月","5月","6月","7月","8月","9月","10月","11月","12月"]
        }
    };
    var methods = {
        init: function (options, callback) {
            var obj = $(this);
            var lang = BaseFunction.GetLanguage();
            var isCn = lang === "cn";
            var setting = $.extend({}, defaults, options || {});
            var ranges;
            if (isCn) {
                ranges = {
                    "今天": [
                        moment().format(setting.format),
                        moment().format(setting.format)
                    ],
                    "昨天": [
                        moment().subtract(1, 'days').format(setting.format),
                        moment().subtract(1, 'days').format(setting.format)
                    ],
                    "近7天": [
                        moment().subtract(6, 'days').format(setting.format),
                        moment().format(setting.format)
                    ],
                    "本月": [
                        moment().startOf('month').format(setting.format),
                        moment().format(setting.format)
                    ],
                    "上月": [
                        moment().month(moment().month() - 1).startOf('month').format(setting.format),
                        moment().month(moment().month() - 1).endOf('month').format(setting.format)
                    ]
                };
            }
            else {
                ranges = {
                    "Today": [
                        moment().format(setting.format),
                        moment().format(setting.format)
                    ],
                    "Yesterday": [
                        moment().subtract(1, 'days').format(setting.format),
                        moment().subtract(1, 'days').format(setting.format)
                    ],
                    "Last 7 Days": [
                        moment().subtract(6, 'days').format(setting.format),
                        moment().format(setting.format)
                    ],
                    "This Month": [
                        moment().startOf('month').format(setting.format),
                        moment().format(setting.format)
                    ],
                    "Last Month": [
                        moment().month(moment().month() - 1).startOf('month').format(setting.format),
                        moment().month(moment().month() - 1).endOf('month').format(setting.format)
                    ]
                };
            }
            var locale = {
                "direction": "ltr",
                "format": setting.format,
                "separator": " ~ ",
                "applyLabel": isCn ? languageresource.applyLabel.cn : languageresource.applyLabel.en,
                "cancelLabel": isCn ? languageresource.cancelLabel.cn : languageresource.cancelLabel.en,
                "fromLabel": isCn ? languageresource.fromLabel.cn : languageresource.fromLabel.en,
                "toLabel": isCn ? languageresource.toLabel.cn : languageresource.toLabel.en,
                "customRangeLabel": isCn ? languageresource.customRangeLabel.cn : languageresource.customRangeLabel.en,
                "daysOfWeek": isCn ? languageresource.daysOfWeek.cn : languageresource.daysOfWeek.en,
                "monthNames": isCn ? languageresource.monthNames.cn : languageresource.monthNames.en,
                "firstDay": 1
            };
            if (setting.singleDatePicker) {
                setting = $.extend({}, setting, { locale: locale });
            }
            else {
                setting = $.extend({}, setting, { ranges: ranges, locale: locale });
            }
            obj.daterangepicker(setting, callback);
        },
        getvalues: function () {
            var result = $(this).data('daterangepicker');
            var formatstr = result.locale.format;
            return {
                startDate: result.startDate.format(formatstr),
                endDate: result.endDate.format(formatstr)
            };
        }
    };
    $.fn.dateranger = function (options, callback) {
        if (options === 'getvalues') {
            return methods.getvalues.call(this);
        }
        return this.each(function () {
            var obj = this;
            if (typeof options === 'object') {
                methods.init.call(obj, options,callback);
            }
            else {
                $.error("dateranger get an invalid parameter");
            }
        });
    };
})(jQuery);