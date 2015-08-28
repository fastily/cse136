function TaViewModel() {

    var TaModelObj = new TaModel();
    var self = this;
    var initialBind = true;
    var taListViewModel = ko.observableArray();

    this.Initialize = function() {

        var viewModel = {
            id: ko.observable(1),
            first: ko.observable("Nick"),
            last: ko.observable("Test"),
            type: ko.observable("Grader"),
            add: function (data) {
                self.CreateTa(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divTa"));
    };

    this.CreateTa = function(data) {
        var model = {
            TaId: data.id(),
            FirstName: data.first(),
            LastName: data.last(),
            TaType: data.type()
        }

        TaModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create Ta successful");
            } else {
                alert("Error creating Ta occurred");
            }
        });

    };

    this.GetAll = function() {

        TaModelObj.GetAll(function(taList) {
            taListViewModel.removeAll();

            for (var i = 0; i < taList.length; i++) {
                taListViewModel.push({
                    id: taList[i].TaId,
                    first: taList[i].FirstName,
                    last: taList[i].LastName,
                    type: taList[i].TaType
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: taListViewModel }, document.getElementById("divTaListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDetail = function (id) {

        TaModelObj.GetDetail(id, function (result) {
            
            var ta = {
                id: result.TaId,
                first: result.FirstName,
                last: result.LastName,
                type: result.TaType
            };

            if (initialBind) {
                ko.applyBindings({ viewModel: ta }, document.getElementById("divTaContent"));
            }
        });
    };

    ko.bindingHandlers.DeleteTa = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                TaModelObj.Delete(id, function(result) {
                    if (result != "ok") {
                        alert("Error Deleting Ta occurred");
                    } else {
                        taListViewModel.remove(viewModel);
                    }
                });
            });
        }
    }
}
