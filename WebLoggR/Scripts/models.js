function message(n, m, t, time) {
    var self = this;

    self.title = ko.observable(n);
    self.message = ko.observable(m);
    self.type = ko.observable(t);
    self.time = ko.observable(time);

    self.typeCSS = ko.computed(function () {

        var result = "";

        switch (self.type()) {
            case "log":
            default:
                result = "";
                break;
            case "warn":
                result = "label-warning";
                break;
            case "critical":
                result = "label-important";
                break;
            case "success":
                result = "label-success";
                break;
        }

        return result;
    });
}

function application(n, i, a) {
    var self = this;

    self.name = ko.observable(n);
    self.id = ko.observable(i);
    self.apiKey = ko.observable(a);

    self.messages = ko.observableArray();

}