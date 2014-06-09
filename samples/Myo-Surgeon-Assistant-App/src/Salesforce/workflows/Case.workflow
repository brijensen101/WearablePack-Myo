<?xml version="1.0" encoding="UTF-8"?>
<Workflow xmlns="http://soap.sforce.com/2006/04/metadata">
    <alerts>
        <fullName>Remember_to_book_your_Post_Op_assessment</fullName>
        <description>Remember to book your Post-Op assessment</description>
        <protected>false</protected>
        <recipients>
            <field>ContactId</field>
            <type>contactLookup</type>
        </recipients>
        <senderType>CurrentUser</senderType>
        <template>unfiled$public/Assessment_Reminder</template>
    </alerts>
    <fieldUpdates>
        <fullName>Reset_Request_Post_Op_Xray_field</fullName>
        <description>Reset the Request_Post_Op_Xray__c field to avoid other field update to trigger a task.</description>
        <field>Request_Post_Op_Xray__c</field>
        <literalValue>0</literalValue>
        <name>Reset Request Post Op Xray field</name>
        <notifyAssignee>false</notifyAssignee>
        <operation>Literal</operation>
        <protected>false</protected>
    </fieldUpdates>
    <rules>
        <fullName>Order Post Op Xray</fullName>
        <actions>
            <name>Reset_Request_Post_Op_Xray_field</name>
            <type>FieldUpdate</type>
        </actions>
        <actions>
            <name>Order_post_op_Xray</name>
            <type>Task</type>
        </actions>
        <active>true</active>
        <criteriaItems>
            <field>Case.Request_Post_Op_Xray__c</field>
            <operation>equals</operation>
            <value>True</value>
        </criteriaItems>
        <description>Create a task reminder for admin staff based on a surgeon performing a hand-gesture whilst wearing a MYO device during an operation.</description>
        <triggerType>onCreateOrTriggeringUpdate</triggerType>
    </rules>
    <rules>
        <fullName>Patient appointment reminder</fullName>
        <actions>
            <name>Remember_to_book_your_Post_Op_assessment</name>
            <type>Alert</type>
        </actions>
        <active>true</active>
        <criteriaItems>
            <field>Case.Status</field>
            <operation>equals</operation>
            <value>Completed</value>
        </criteriaItems>
        <description>At  the completion of a surgery, send the patient an email reminder to book their post-op check-up and assessment</description>
        <triggerType>onCreateOrTriggeringUpdate</triggerType>
    </rules>
    <tasks>
        <fullName>Order_post_op_Xray</fullName>
        <assignedTo>aalammar@myo.com</assignedTo>
        <assignedToType>user</assignedToType>
        <description>Order the post-op Xray in time for the post-op assessment. 
The X-ray must be taken between 5-8 days after the surgery.</description>
        <dueDateOffset>5</dueDateOffset>
        <notifyAssignee>true</notifyAssignee>
        <offsetFromField>Case.Surgery_Date__c</offsetFromField>
        <priority>Normal</priority>
        <protected>false</protected>
        <status>Not Started</status>
        <subject>Order post-op Xray</subject>
    </tasks>
</Workflow>
