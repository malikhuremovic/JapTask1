import FormModal from '../FormModal';

import classes from '../Style/ActionForms.module.css';

const StudentActionForms = ({
  formModel,
  handleStudentFormInput,
  handleAddStudent,
  handleEditStudent,
  handleDeleteStudent,
  handleAddState,
  handleEditState,
  handleDeleteState,
  actionState,
  studentFormData,
  availableSelections
}) => {
  return (
    <div className={classes.action_form}>
      {actionState.action === 'add' && actionState.show && (
        <FormModal
          title="Add student"
          formType="add"
          formModel={formModel}
          handleState={handleAddState}
          handleFormSubmission={handleAddStudent}
          handleFormInput={handleStudentFormInput}
          formData={studentFormData}
          availableItems={availableSelections}
        />
      )}
      {actionState.action === 'edit' && actionState.show && (
        <FormModal
          title="Edit student"
          formType="edit"
          formModel={formModel}
          handleState={handleEditState}
          handleFormSubmission={handleEditStudent}
          handleFormInput={handleStudentFormInput}
          formData={studentFormData}
          availableItems={availableSelections}
        />
      )}
      {actionState.action === 'delete' && actionState.show && (
        <FormModal
          formType="delete"
          title="Delete student"
          formModel={formModel}
          handleState={handleDeleteState}
          handleDelete={handleDeleteStudent}
        />
      )}
    </div>
  );
};

export default StudentActionForms;
