function DateModel() {
	this.endDatePopupOpened = false;
	this.selectedEndDate = null;
	this.selectedStartDate = null;
	this.startDatePopupOpened = false;
}

DateModel.prototype.resetValues = function() {
	this.endDatePopupOpened = false;
	this.selectedEndDate = null;
	this.selectedStartDate = null;
	this.startDatePopupOpened = false;
};