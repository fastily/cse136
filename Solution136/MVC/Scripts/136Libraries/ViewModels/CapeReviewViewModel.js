function CapeReviewViewModel() {
    var CapeReviewModelObj = new CapeReviewModel();
    var self = this;
    var initialBind = true;
    var capereviewListViewModel = ko.observableArray();
  

    this.Initialize = function(student_id) {

        var viewModel = {
            Studnet_id: student_id,
            id: ko.observable(1),
            course: ko.observable("CSE 3"),
            desired: ko.observable("A+"),
            reason: ko.observable("Reason for your request"),
            add: function (data) { 
                self.AddCapeReview(data, student_id);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudent"));
    };

    this.AddCapeReview = function (data, student_id) {

        var model = {
            /*
            CapeReviewId: 1,
            Student_id: "1",
            Schedule_id: 1,
            Approved: false,
            Course_id: 1,
            Desired: "A-",
            Description: "fffffffffffffffffuuuuuu"
           */
     
            CapeReviewId: data.id(),
            Student_id: student_id,
            Schedule_id: 1, //data.Schedule_id,
            Approved: false,
            Course_id: "1", //data.Course_id,
            desired: data.desired(),
            Description: data.reason()
        }

        CapeReviewModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create CapeReview successful");
            } else {
                alert("Error creating CapeReview occurred");
            }
        });

    };


    this.Approve = function (capereview) {
        var CapeReviewModelObj = new CapeReviewModel();

        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var capereviewData = {
            Approved: true
        };

        CapeReviewModelObj.Approve(capereviewData, function (message) {
            $('#divMessage').html(message);
        });

    };

    /*
    this.GetAll = function() {
        CapeReviewModelObj.GetAll(function(capereviewList) {
            capereviewListViewModel.removeAll();

            for (var i = 0; i < capereviewList.length; i++) {
                capereviewListViewModel.push({
                    id: capereviewList[i].CapeReviewId,
                    first: capereviewList[i].FirstName,
                    last: capereviewList[i].LastName,
                    type: capereviewList[i].CapeReviewType
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: capereviewListViewModel }, document.getElementById("divCapeReviewListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDetail = function (id) {

        CapeReviewModelObj.GetDetail(id, function (result) {
            
            var capereview = {
                id: result.CapeReviewId,
                first: ko.observable(result.FirstName),
                last: ko.observable(result.LastName),
                type: ko.observable(result.CapeReviewType),
                update: function () {
                    self.UpdateCapeReview(this);
                }
            };

            if (initialBind) {
                ko.applyBindings(capereview, document.getElementById("divCapeReviewContent"));
            }
        });
    };
    */
}
