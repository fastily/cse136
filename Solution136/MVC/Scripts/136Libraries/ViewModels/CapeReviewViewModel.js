function CapeReviewViewModel() {
    var CapeReviewModelObj = new CapeReviewModel();
    var self = this;
    var initialBind = true;
    var capereviewListViewModel = ko.observableArray();
  

    this.Initialize = function(sid, iname, cname) {

        var viewModel = {
            cname: ko.observable(cname),
            irating: ko.observable(), 
            crating: ko.observable(),
            reason: ko.observable("Reason for your request"),
            add: function (data) {
                self.AddCapeReview(data, sid, iname, cname);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudentEnrollments"));
    };

    this.AddCapeReview = function (data, sid, iname, cname) {

        var model = {
            id: sid,
            irating: data.irating(),
            cname: cname,
            crating: data.crating(),
            reason: data.reason()
        };

        CapeReviewModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create CapeReview successful");
            } else {
                alert("Error creating CapeReview occurred");
            }
        });

    };

}
