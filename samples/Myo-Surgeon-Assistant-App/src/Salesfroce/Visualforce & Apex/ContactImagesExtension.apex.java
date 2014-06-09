public with sharing class ContactImagesExtension{
 
    private ApexPages.standardController controller;
    private Contact patient;
    private Attachment photo;
  
    public ContactImagesExtension(ApexPages.StandardController controller) {
        this.controller = controller;
        this.patient = (Contact)controller.getRecord();
    }
 
    public Id patientPhotoId {
        get {
            if(photo == null){
                try{
                	this.photo = [SELECT Id, Name FROM Attachment WHERE ParentId = :patient.Id AND Name = 'ProfilePic' LIMIT 1][0];   
                }
                catch (Exception e){
                    // In a production system, a thorough exception handling shoud be applied here.
                    System.Debug(e.getMessage());
                }
                 
            }
            return this.photo.Id;
        }
    }
}
