function RequireCategory_Check(src, args) {
    var numSelectedItems = 0;

    numSelectedItems += $("#" + ddlMensCategory_id + " option:selected").length;
    numSelectedItems += $("#" + ddlWomensCategory_id + " option:selected").length;
    numSelectedItems += $("#" + ddlKidsCategory_id + " option:selected").length;

    if (numSelectedItems <= 0) {
        args.IsValid = false;
    }

}