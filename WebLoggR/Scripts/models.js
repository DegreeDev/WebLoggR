function message(n, m, t, time) {
    var self = this;

    self.title = ko.observable(n);
    self.message = ko.observable(m);
    self.type = ko.observable(t);
    self.time = ko.observable(time);

    self.typeCSS = ko.computed(function () {
        return self.type() == "log" ? "" : self.type() == "warn" ? "label-warning" : "label-important";
    });
}

function application(n, i, a) {
    var self = this;

    self.name = ko.observable(n);
    self.id = ko.observable(i);
    self.apiKey = ko.observable(a);

    self.messages = ko.observableArray();

}