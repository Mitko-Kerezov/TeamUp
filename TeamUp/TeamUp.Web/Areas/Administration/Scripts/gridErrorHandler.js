window.CategoryAndSkill_Error = function (args) {
    if (args.errors) {
        var grid = $("#skills-grid").data("kendoGrid");
        var validationTemplate = kendo.template($("#validationMessageTemplate").html());
        grid.one("dataBinding", function (e) {
            e.preventDefault();

            $.each(args.errors, function (propertyName) {
                var renderedTemplate = validationTemplate({ field: propertyName, messages: this.errors });
                console.log(grid.editable.element);
                grid.editable.element.find(".errors").append(renderedTemplate);
            });
        });
    }
};