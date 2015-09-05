function GradeChangeViewModel() {
    var GradeChangeModelObj = new GradeChangeModel();
    var self = this;
    var initialBind = true;
    var gradechangeListViewModel = ko.observableArray();
  

    this.Initialize = function(student_id) {

        var viewModel = {
            Studnet_id: student_id,
            id: ko.observable(1),
            reason: ko.observable("Reason for your request"),
            add: function (data) { 
                self.AddGradeChange(data, student_id);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudent"));
    };

    this.AddGradeChange = function(data, student_id) {
        var model = {
            GradeChangeId: 1,
            Student_id: "1",
            Schedule_id: 1,
            Approved: false,
            Course_id: 1,
            Description: "fffffffffffffffffuuuuuu"
            /*
            GradeChangeId: data.id(),
            Student_id: student_id,
            Schedule_id: data.Schedule_id,
            Approved: false,
            Course_id: data.Course_id,
            Description: data.reason()
            */
        }

        GradeChangeModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create GradeChange successful");
            } else {
                alert("Error creating GradeChange occurred");
            }
        });

    };


    this.Approve = function (gradechange) {
        var GradeChangeModelObj = new GradeChangeModel();

        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var gradechangeData = {
            Approved: true
        };

        GradeChangeModelObj.Approve(gradechangeData, function (message) {
            $('#divMessage').html(message);
        });

    };

    /*
    this.GetAll = function() {
        GradeChangeModelObj.GetAll(function(gradechangeList) {
            gradechangeListViewModel.removeAll();

            for (var i = 0; i < gradechangeList.length; i++) {
                gradechangeListViewModel.push({
                    id: gradechangeList[i].GradeChangeId,
                    first: gradechangeList[i].FirstName,
                    last: gradechangeList[i].LastName,
                    type: gradechangeList[i].GradeChangeType
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: gradechangeListViewModel }, document.getElementById("divGradeChangeListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDetail = function (id) {

        GradeChangeModelObj.GetDetail(id, function (result) {
            
            var gradechange = {
                id: result.GradeChangeId,
                first: ko.observable(result.FirstName),
                last: ko.observable(result.LastName),
                type: ko.observable(result.GradeChangeType),
                update: function () {
                    self.UpdateGradeChange(this);
                }
            };

            if (initialBind) {
                ko.applyBindings(gradechange, document.getElementById("divGradeChangeContent"));
            }
        });
    };
    */
}
