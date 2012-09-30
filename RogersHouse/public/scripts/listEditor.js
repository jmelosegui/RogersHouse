(function ($) {

    $.fn.inlineEdit = function (options) {

        options = $.extend({
            hover: 'hover',
            value: '',
            save: '',
            buttonText: 'Save',
            placeholder: 'Click to edit'
        }, options);

        return $.each(this, function () {
            $.inlineEdit(this, options);
        });
    }

    $.inlineEdit = function (obj, options) {
        var self = $(obj),
            placeholderHtml = '<span class="inlineEdit-placeholder">' + options.placeholder + '</span>';

        self.value = function (newValue) {
            if (arguments.length) {
                self.data('value', $(newValue).hasClass('inlineEdit-placeholder') ? '' : newValue);
            }
            return self.data('value');
        }

        self.value($.trim(self.text()) || options.value);

        self.bind('click', function (event) {
            var $this = $(event.target);

            if ($this.is(':button')) {
                var hash = {
                    value: $input = $this.siblings('input').val()
                };

                if (($.isFunction(options.save) && options.save.call(self, event, hash)) !== false || !options.save) {
                    self.value(hash.value);
                }

            } else if ($this.is(self[0].tagName) || $this.hasClass('inlineEdit-placeholder')) {
                self
                    .html('<input type="text" value="' + self.value() + '"/> <input type="submit" value="'+options.buttonText+'"/>')
                    .find(':text')
                        .bind('blur', function () {
                            if (self.timer) {
                                window.clearTimeout(self.timer);
                            }
                            self.timer = window.setTimeout(function () {
                                self.html(self.value() || placeholderHtml);
                                self.removeClass(options.hover);
                            }, 200);
                        })
                        .focus();
            }
        })
        .hover(
            function () {
                $(this).addClass(options.hover);
            },
            function () {
                $(this).removeClass(options.hover);
            }
        );

        if (!self.value()) {
            self.html($(placeholderHtml));
        } else if (options.value) {
            self.html(options.value);
        }
    }

})(jQuery);

$(function () {
    $('.editable').inlineEdit({
        save: function (e, data) {
            $.ajax({
                url: this.href + data.value,
                cache: false
            });
            return false;
        } 
     });
});

$("#addItem").click(function () {
    $.ajax({
        url: this.href,
        cache: false,
        success: function (html) {
            $("#editorRows").append(html);
        }
    });
    return false;
});

$("a.deleteRow").live("click", function () {
    $(this).parents("div.editorRow:first").remove();
    return false;
});
