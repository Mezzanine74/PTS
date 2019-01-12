
function BodyText(id, Ref, Eng, Rus, GenerateCodeASPX, GenerateCodeVB) {
        var self = this;
        self.Id = id
        self.Ref = Ref
        self.Eng = ko.observable(Eng)
        self.Rus = ko.observable(Rus)
        self.GenerateCodeASPX = ko.observable(GenerateCodeASPX)
        self.GenerateCodeVB = ko.observable(GenerateCodeVB)
        
        self.addBodyText = function () {
            var dataObject = ko.toJSON(self);

            delete dataObject.GenerateCodeASPX;
            delete dataObject.GenerateCodeVB;

            $.ajax({
                url: '/BodyTexts/Create',
                type: 'post',
                data: dataObject,
                contentType: 'application/json',
                success: function (data) {

                    $.each(data, function (key, value) {
                        global.model2.BodyTexts.unshift(
                            new BodyText(value.Id, value.Ref, value.Eng, value.Rus, value.GenerateCodeASPX, value.GenerateCodeVB)
                        );
                    })

                    self.Id = null
                    self.Ref = ''
                    self.Eng('')
                    self.Rus('')
                    self.GenerateCodeASPX('')
                    self.GenerateCodeVB('')
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

        self.updateBodyText = function (item) {
            var dataObject = ko.toJSON(item);

            delete dataObject.GenerateCodeASPX;
            delete dataObject.GenerateCodeVB;

            $.ajax({
                url: '/BodyTexts/Edit/',
                type: 'post',
                data: dataObject,
                contentType: 'application/json',
                success: function (data) {
                    console.log(data);

                                $.gritter.add({
                                    title: 'Updated',
                                    text: 'Thanks for your support (:',
                                    time: '1500',
                                    class_name: 'gritter-info gritter-top-center'
                                })

                },
                error: function (error) {
                    console.log(error);
                }
            });

        }

        self.altEnterSubmit = function (data, event) {

            if (event.ctrlKey && event.keyCode == 13) {
                // submit form on Ctl + Enter
                console.log("Ctrl + enter")
                //$("#formEngEnter").submit();
                document.getElementById("btnSubmit").click();
            }

            return true;
        }
}

function BodyTextList() {
    var self = this;
    self.BodyTexts = ko.observableArray([]);

    self.getBodyTexts = function () {
        self.BodyTexts.removeAll();

        //JSON girdisi ile datanin cekilis sekli
        $.getJSON('/BodyTexts/IndexJson', function (data) {
            $.each(data, function (key, value) {
                self.BodyTexts.push(new BodyText(value.Id, value.Ref, value.Eng, value.Rus, value.GenerateCodeASPX, value.GenerateCodeVB))
            })
        })
    }

    self.RemoveBodyText = function (bodyText) {
            $.ajax({
                url: '/BodyTexts/Delete/' + bodyText.Id,
                type: 'post',
                contentType: 'application/json',
                success: function (data) {
                    self.BodyTexts.remove(bodyText);
                },
                error: function (error) {
                    console.log(error);
                }
            });
    }
}

var global
global = { model1: new BodyText(0, '', '', '', '', ''), model2: new BodyTextList() }

$(function () {
    ko.applyBindings(global);
    global.model2.getBodyTexts();

})

