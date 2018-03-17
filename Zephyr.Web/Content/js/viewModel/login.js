var viewModel = function () {
    var self = this;
    this.form = {
        usercode: ko.observable(),
        password: ko.observable(),
        remember: ko.observable(false),
        ip: null,
        city: null
    };
    this.message = ko.observable();
    this.loginClick = function (form) {
        if (!self.form.password())
            self.form.password($('[type=password]').val());
        $.ajax({
            type: "POST",
            url: "/login/doAction",
            data: ko.toJSON(self.form),
            dataType: "json",
            contentType: "application/json",
            success: function (d) {
                if (d.status == 'success') {
                    self.message("登陆成功正在跳转，请稍候...");
                    window.location.href = '/';
                } else {
                    self.message(d.message);
                }
            },
            error: function (e) {
                self.message(e.responseText);
            },
            beforeSend: function () {
                $(form).find("input").attr("disabled", true);
                self.message("正在登陆处理，请稍候...");
            },
            complete: function () {
                $(form).find("input").attr("disabled", false);
            }
        });
    };

    this.resetClick = function () {
        self.form.usercode("");
        self.form.password("");
        self.form.remember(false);
    };

    this.init = function () {
        //ILData[0]:IP地址;ILData[1]:IP地址的类型;ILData[2]:IP地址的省市;ILData[4]:IP地址的运营商
        var ILData = ILData || [];
        self.form.ip = ILData[0];
        $.getJSON("http://api.map.baidu.com/location/ip?ak=F454f8a5efe5e577997931cc01de3974&callback=?", function (d) {
            self.form.city = d.content.address;
        });
        if (top != window) top.window.location = window.location;
    };

    this.init();
};

$(function () { ko.applyBindings(new viewModel());});