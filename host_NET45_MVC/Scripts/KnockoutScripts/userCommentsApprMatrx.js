
function Addendums_UserRemark(id, addendumId, userName, remark, lastUpdate, pageUserName, itemMode, editMode, showOrHide) {

    var self = this;

    self.Me = ko.observableArray([])

    self.CreateMe = function (id, addendumId, userName, remark, lastUpdate, pageUserName, showOrHide) {
        self.Me.removeAll();

        var object = new Addendums_UserRemark();

        object.Id = id;
        object.AddendumId(addendumId);
        object.UserName(userName);
        object.PageUserName(pageUserName);
        object.Remark(remark);
        object.LastUpdate = lastUpdate;
        object.ShowOrHide(showOrHide);
        object.ItemMode(true);
        object.EditMode(false);

        self.Me.push(object);
    }

    self.Id = id
    self.AddendumId = ko.observable(addendumId)
    self.UserName = ko.observable(userName)
    self.PageUserName = ko.observable(pageUserName)
    self.Remark = ko.observable(remark)
    self.LastUpdate = lastUpdate

    self.ItemMode = ko.observable(itemMode);
    self.EditMode = ko.observable(editMode);

    self.ChangeModeToEdit = function (item) {
        console.log(item);
        item.ItemMode(false);
        item.EditMode(true);
    }

    self.Add = function (item) {

        var itemobject = JSON.parse(ko.toJSON(item));
        var Addendums_UserRemarkParameters = {};
        Addendums_UserRemarkParameters.AddendumID = itemobject.AddendumId;
        Addendums_UserRemarkParameters.UserName = itemobject.UserName;
        Addendums_UserRemarkParameters.Remark = itemobject.Remark;

        $.ajax({
            url: '/UserRemarks/CreateAddendum',
            type: 'post',
            data: JSON.stringify(Addendums_UserRemarkParameters),
            contentType: 'application/json',
            success: function (data) {

                global.Addendums_UserRemarkListModel.getComments(itemobject.AddendumId);

            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    self.Delete = function (item) {

        console.log(item);

        var object = {};
        object.id = JSON.parse(ko.toJSON(item)).Id;
        console.log(object);

        $.ajax({
            url: '/UserRemarks/DeleteAddendum',
            type: 'post',
            data: JSON.stringify(object),
            contentType: 'application/json',
            success: function (data) {

                console.log(data);

                global.Addendums_UserRemarkListModel.Comments.remove(item);

            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    self.Update = function (item) {
        var itemobject = JSON.parse(ko.toJSON(item));
        var Addendums_UserRemarkParameters = {};
        Addendums_UserRemarkParameters.Id = itemobject.Id;
        Addendums_UserRemarkParameters.Remark = itemobject.Remark;

        $.ajax({
            url: '/UserRemarks/EditAddendum',
            type: 'post',
            data: JSON.stringify(Addendums_UserRemarkParameters),
            contentType: 'application/json',
            success: function (data) {

                console.log(data);

                item.Remark(data.Remark);
                item.LastUpdate = data.LastUpdate;
                item.ItemMode(true);
                item.EditMode(false);

            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    self.ShowOrHide = ko.observable(showOrHide)

}

function Contracts_UserRemark(id, contractId, userName, remark, lastUpdate, pageUserName, itemMode, editMode, showOrHide) {

    var self = this;

    self.Me = ko.observableArray([])

    self.CreateMe = function (id, contractId, userName, remark, lastUpdate, pageUserName, showOrHide) {
        self.Me.removeAll();

        var object = new Contracts_UserRemark();

        object.Id = id;
        object.ContractId(contractId);
        object.UserName(userName);
        object.PageUserName(pageUserName);
        object.Remark(remark);
        object.LastUpdate = lastUpdate;
        object.ShowOrHide(showOrHide);
        object.ItemMode(true);
        object.EditMode(false);

        self.Me.push(object);
    }

    self.Id = id
    self.ContractId = ko.observable(contractId)
    self.UserName = ko.observable(userName)
    self.PageUserName = ko.observable(pageUserName)
    self.Remark = ko.observable(remark)
    self.LastUpdate = lastUpdate

    self.ItemMode = ko.observable(itemMode);
    self.EditMode = ko.observable(editMode);

    self.ChangeModeToEdit = function (item) {
        console.log(item);
        item.ItemMode(false);
        item.EditMode(true);
    }

    self.Add = function (item) {

        var itemobject = JSON.parse(ko.toJSON(item));
        var Contracts_UserRemarkParameters = {};
        Contracts_UserRemarkParameters.ContractID = itemobject.ContractId;
        Contracts_UserRemarkParameters.UserName = itemobject.UserName;
        Contracts_UserRemarkParameters.Remark = itemobject.Remark;

        $.ajax({
            url: '/UserRemarks/CreateContract',
            type: 'post',
            data: JSON.stringify(Contracts_UserRemarkParameters),
            contentType: 'application/json',
            success: function (data) {

                global.Contracts_UserRemarkListModel.getComments(itemobject.ContractId);

            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    self.Delete = function (item) {

        console.log(item);

        var object = {};
        object.id = JSON.parse(ko.toJSON(item)).Id;
        console.log(object);

        $.ajax({
            url: '/UserRemarks/DeleteContract',
            type: 'post',
            data: JSON.stringify(object),
            contentType: 'application/json',
            success: function (data) {

                console.log(data);

                global.Contracts_UserRemarkListModel.Comments.remove(item);

            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    self.Update = function (item) {
        var itemobject = JSON.parse(ko.toJSON(item));
        var Contracts_UserRemarkParameters = {};
        Contracts_UserRemarkParameters.Id = itemobject.Id;
        Contracts_UserRemarkParameters.Remark = itemobject.Remark;

        $.ajax({
            url: '/UserRemarks/EditContract',
            type: 'post',
            data: JSON.stringify(Contracts_UserRemarkParameters),
            contentType: 'application/json',
            success: function (data) {

                console.log(data);

                item.Remark(data.Remark);
                item.LastUpdate = data.LastUpdate;
                item.ItemMode(true);
                item.EditMode(false);

            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    self.ShowOrHide = ko.observable(showOrHide)

}

function Addendums_UserRemarkList() {
    var self = this;
    self.Comments = ko.observableArray([]);

    self.getComments = function (id) {
        self.Comments.removeAll();

        //JSON girdisi ile datanin cekilis sekli
        $.getJSON('/UserRemarks/GetByAddendumId/?id=' + id, function (data) {
            $.each(data, function (key, value) {
                var object = new Addendums_UserRemark();

                object.Id = value.id;
                object.AddendumId(value.AddendumID);
                object.UserName(value.UserName);
                object.PageUserName(value.PageUserName);
                object.Remark(value.Remark);
                object.LastUpdate = value.LastUpdate;
                object.ShowOrHide(value.ShowOrHide);
                object.ItemMode(true);
                object.EditMode(false);

                self.Comments.push(object)
            })
        })
    }
}

function Contracts_UserRemarkList() {
    var self = this;
    self.Comments = ko.observableArray([]);

    self.getComments = function (id) {
        self.Comments.removeAll();

        //JSON girdisi ile datanin cekilis sekli
        $.getJSON('/UserRemarks/GetByContractId/?id=' + id, function (data) {
            $.each(data, function (key, value) {
                var object = new Contracts_UserRemark();

                object.Id = value.id;
                object.ContractId(value.ContractID);
                object.UserName(value.UserName);
                object.PageUserName(value.PageUserName);
                object.Remark(value.Remark);
                object.LastUpdate = value.LastUpdate;
                object.ShowOrHide(value.ShowOrHide);
                object.ItemMode(true);
                object.EditMode(false);

                self.Comments.push(object)
            })
        })
    }
}

var global
global = {
    Contracts_UserRemarkModel: new Contracts_UserRemark(),
    Contracts_UserRemarkListModel: new Contracts_UserRemarkList(),
    Addendums_UserRemarkModel: new Addendums_UserRemark(),
    Addendums_UserRemarkListModel: new Addendums_UserRemarkList()
}

$(function () {
    ko.applyBindings(global);
})