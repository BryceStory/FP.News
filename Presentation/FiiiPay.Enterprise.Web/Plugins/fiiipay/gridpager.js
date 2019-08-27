(function ($) {
    var defaults = {
        currentpage: 1,//当前页
        totalpage: 0,//总页数
        totalcount: 0,//总记录数
        pagesize: 10,//每页记录数
        orderby: 'asc',//排序方式
        shownum: 7,
        type: "post"
    };
    var languageresourcecn = {
        norecord:'暂无记录',
        gototext: '转到第{toreplace}页',
        gotobtntext: '转跳',
        totalpagetext: '共{toreplace}页',
        totalrecordtext: '共{toreplace}条记录'
    };
    var languageresourceen = {
        norecord: 'No Record',
        gototext: 'Go to page {toreplace}',
        gotobtntext: 'Go',
        totalpagetext: 'Total {toreplace} pages',
        totalrecordtext: 'Total {toreplace} records'
    };
    function initgrid(grid, setting) {
        grid.addClass("f-grid");
        var colmodel = setting.colmodel;
        if (colmodel) {
            var theaderstr = "<thead><tr>";
            var colmodellength = getshowmodellength(colmodel);
            var widthstr = (100 / colmodellength).toString().match(/^\d+(?:\.\d{0,2})?/) + "%";
            var alginstr = "center";
            for (var i = 0; i < colmodel.length; i++) {
                var width;
                var algin;
                var cloumnHidden;
                var hideinxsclass;
                if (colmodel[i].width) {
                    width = colmodel[i].width + "%";
                }
                else {
                    width = widthstr;
                }
                if (colmodel[i].algin) {
                    algin = colmodel[i].algin;
                }
                else {
                    algin = alginstr;
                }
                if (colmodel[i].hideinxs) {
                    hideinxsclass = " hidden-xs";
                }
                else {
                    hideinxsclass = "";
                }
                if (colmodel[i].hidden) {
                    cloumnHidden = " hidden"
                }
                else {
                    cloumnHidden = ""
                }
                if (i < colmodel.length - 1) {
                    theaderstr += '<th class="text-' + algin + hideinxsclass + cloumnHidden + '" width="' + width + '">';
                }
                else {
                    theaderstr += '<th class="text-' + algin + hideinxsclass + cloumnHidden + '">';
                }
                if (setting.language === "cn") {
                    theaderstr += colmodel[i].titlecn;
                }
                else {
                    theaderstr += colmodel[i].title;
                }
                theaderstr += "</th>";
            }
            theaderstr += "</tr></thead>";
            grid.html(theaderstr);
            gridgotopage(grid, setting.currentpage, setting);
        }
    }
    function getshowmodellength(colmodel) {
        var j = 0;
        for (var i = 0; i < colmodel.length; i++) {
            if (!colmodel[i].hideinxs) {
                j++;
            }
        }
        return j;
    }
    function initpager(pager, setting) {
        pager.addClass("f-pager");
        var $ulpager = $('<ul class="pagination"></ul>');
        pager.html($ulpager);
        if (setting.totalpage <= 0) {
            return false;
        }
        refreshpager($ulpager, setting);
        initevent($ulpager);
    }
    function refreshpager($ul, setting) {
        if (setting.totalpage <= 0) {
            $ul.empty();
        }
        else {
            $ul.html(initpagerhtml(setting));
            $ul.append(inittotolhtml(setting));
        }
    }
    function inittotolhtml(setting) {
        var langresource;
        var toreplacereg = new RegExp("\\{toreplace\\}", "g");
        if (setting.language === "cn") {
            langresource = languageresourcecn;
        }
        else {
            langresource = languageresourceen;
        }
        var htmlstr = '';
        var gotopageinputstr = '<span><input type="text" value="' + setting.currentpage + '" /></span><span>';
        htmlstr += '<li><span class="totalpage"><span><span class="hidden-xs" style="margin-left:20px;">' + langresource.gototext.replace(toreplacereg, gotopageinputstr) + '</span>';
        htmlstr += '<span><button>' + langresource.gotobtntext + '</button></span></span>';
        htmlstr += '<span style="margin-left:40px;">' + langresource.totalpagetext.replace(toreplacereg, setting.totalpage) + '，</span>';
        htmlstr += '<span style="margin-left:10px;">' + langresource.totalrecordtext.replace(toreplacereg, setting.totalcount) + '</span>';
        htmlstr += '</span></li>';
        return htmlstr;
    }
    function initpagerhtml(setting) {
        var pagerstr = '';
        if (setting.currentpage === 1) {
            pagerstr += '<li class="disabled"><span aria-hidden="true">&laquo;</span></li>';
        }
        else {
            pagerstr += '<li><a href="javascript:" data-increment=-1><span aria-hidden="true">&laquo;</span></a></li>';
        }
        var pagercount = parseInt(setting.shownum / 2);
        var prevlevel = setting.currentpage - pagercount;
        var nextlevel = setting.currentpage + pagercount;
        if (prevlevel < 0) {
            nextlevel = nextlevel - prevlevel;
            prevlevel = 0;
        }
        else if (nextlevel > setting.totalpage) {
            prevlevel = prevlevel + setting.totalpage - nextlevel;
            nextlevel = setting.totalpage;
        }
        for (var i = 1; i <= setting.totalpage; i++) {
            if (i === prevlevel && setting.currentpage > pagercount + 1) {
                pagerstr += '<li class="more"><span>...</span></li>';
            }
            if (i === setting.currentpage) {
                pagerstr += '<li class="active"><a href="javascript:" data-increment=0>' + i + '</a></li>';
            }
            else if (i >= prevlevel && i <= nextlevel) {
                pagerstr += '<li><a href="javascript:" data-increment=0>' + i + '</a></li>';
            }
            if (i === nextlevel && setting.currentpage < (setting.totalpage - pagercount)) {
                pagerstr += '<li class="more"><span>...</span></li>';
            }
        }
        if (setting.currentpage === setting.totalpage) {
            pagerstr += '<li class="disabled"><span aria-hidden="true">&raquo;</span></li>';
        }
        else {
            pagerstr += '<li><a href="javascript:" data-increment=1><span aria-hidden="true">&raquo;</span></a></li>';
        }
        return pagerstr;
    }
    function initevent($ul) {
        var $grid = $ul.parent().prev();
        var setting = $grid.data("currentsetting");
        $ul.on("click", "a", function () {
            var $link = $(this);
            var incrementvalue = Number($link.data("increment"));
            var page = setting.currentpage;
            if (incrementvalue === -1) {
                page = setting.currentpage - 1;
            }
            else if (incrementvalue === 1) {
                page = setting.currentpage + 1;
            }
            else if (incrementvalue === 0) {
                page = Number($link.text());
            }
            gridgotopage($grid, page, setting);
            refreshpager($ul, setting);
        });
        $ul.on("click", "button", function () {
            var $btn = $(this);
            var page = Number($btn.closest("li").find('input[type="text"]').val());
            if (!page) {
                return;
            }
            if (page > setting.totalpage) {
                return;
            }
            gridgotopage($grid, page, setting);
            refreshpager($ul, setting);
        });
        $ul.on("keypress", 'input[type="text"]', function () {
            if (event.keyCode === 13) {
                var $text = $(this);
                var page = Number($text.val());
                if (!page) {
                    return;
                }
                if (page > setting.totalpage) {
                    return;
                }
                gridgotopage($grid, page, setting);
                refreshpager($ul, setting);
                return false;
            }
            return true;
        });
    }
    function gridgotopage($grid, page, setting) {
        var postdata = setting.params || {};
        postdata.Page = page;
        postdata.Size = setting.pagesize;
        postdata.SortColumn = setting.sortcolumn;
        postdata.OrderBy = setting.orderby;
        if (setting.type === "post" && setting.url) {
            $.ajax({
                url: setting.url,
                data: postdata,
                async: false,
                success: function (context) {
                    
                    setting.currentpage = context.page;
                    setting.totalpage = context.totalpage;
                    setting.totalcount = context.totalcount;
                    if (context.rows) {
                        var $tbody = $grid.find("tbody");
                        if ($tbody.length <= 0) {
                            $tbody = $("<tbody></tbody>");
                            $grid.append($tbody);
                        }
                        var trstring = "";
                        var alginstr = "center";
                        if (context.rows.length > 0) {
                            for (var i = 0; i < context.rows.length; i++) {
                                trstring += "<tr>";
                                var j = 0;
                                $.each(context.rows[i], function (k, v) {
                                    var algin;
                                    var hideinxsclass;
                                    var cloumnHidden;
                                    if (setting.colmodel[j].algin) {
                                        algin = setting.colmodel[j].algin;
                                    }
                                    else {
                                        algin = alginstr;
                                    }
                                    if (setting.colmodel[j].hideinxs) {
                                        hideinxsclass = " hidden-xs";
                                    }
                                    else {
                                        hideinxsclass = "";
                                    }
                                    if (setting.colmodel[j].hidden) {
                                        cloumnHidden = " hidden"
                                    }
                                    else {
                                        cloumnHidden = ""
                                    }
                                    trstring += '<td class="text-' + algin + hideinxsclass + cloumnHidden + '">';
                                    if (setting.colmodel[j].formatter) {
                                        trstring += setting.colmodel[j].formatter.call($tbody, v, context.rows[i]);
                                    }
                                    else {
                                        trstring += v;
                                    }
                                    trstring += "</td>";
                                    j++;
                                });
                                trstring += "</tr>";
                            }
                            $tbody.html(trstring);
                        }
                        else {
                            var emptyrecordnotice;
                            if (setting.language === "cn") {
                                emptyrecordnotice = languageresourcecn.norecord;
                            }
                            else {
                                emptyrecordnotice = languageresourceen.norecord;
                            }
                            var columncount = setting.colmodel.length;
                            var emptytrstring = '<tr><td colspan="' + columncount + '" class="f-grid-norecord">' + emptyrecordnotice + '</td></tr>';
                            $tbody.html(emptytrstring);
                        }
                    }
                    $grid.data("currentsetting", setting);
                    if (setting.callback) {
                        setting.callback.call($grid, context);
                    }
                }
            });
        }
    }
    var methods = {
        init: function (options) {
            var obj = $(this);
            var setting = $.extend({}, defaults, options[0], { language: BaseFunction.GetLanguage()} || {});
            obj.data("currentsetting", setting);
            obj.wrap('<div class="gridpager"></div>');
            var $pager = $('<div></div>');
            obj.after($pager);
            var $grid = obj;
            if (!obj.is("table")) {
                return false;
            }
            initgrid($grid, setting);
            initpager($pager, setting);
        },
        getpagerinfo: function () {
            var currentsetting = $(this).data("currentsetting");
            var result = { currentpage: currentsetting.currentpage, totalpage: currentsetting.totalpage, totalcount: currentsetting.totalcount, pagesize: currentsetting.pagesize, params: currentsetting.params };
            return result;
        },
        refresh: function (options) {
            if (!options)
                return;
            if (options.length < 2)
                return;
            var currentsetting = $(this).data("currentsetting");
            currentsetting.params = options[1];
            var $grid = $(this);
            var $pager = $(this).parent().children('.f-pager').children("ul.pagination");

            gridgotopage($grid, 1, currentsetting);
            refreshpager($pager, currentsetting);
        }
    };
    $.fn.gridpager = function () {
        var options = arguments;
        var option = options[0];
        if (option === 'getpagerinfo') {
            return methods["getpagerinfo"].call(this, options);
        }

        return this.each(function () {
            var obj = this;
            if (typeof option === 'object' || !option) {
                methods.init.call(obj, options);
            }
            else if (typeof option === 'string') {
                methods[option].call(this, options);
            }
            else {
                $.error("gridpager get an invalid parameter");
            }
        });
    };
})(jQuery);