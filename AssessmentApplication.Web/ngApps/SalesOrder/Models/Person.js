function Person(businessEntitiyId, title, firstName, middleName, lastName, suffix) {
	this.businessEntityId = businessEntitiyId;
	this.title = title;
	this.firstName = firstName;
	this.middleName = middleName;
	this.lastName = lastName;
	this.suffix = suffix;
	this.fullName = [title, firstName, middleName, lastName, suffix]
		.filter(function(val) {
			if (val) {
				return val;
			}
		})
		.join(" ");
}