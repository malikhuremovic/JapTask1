import StudentForm from './StudentForm';
import StudentDeleteForm from './StudentDeleteForm';

import classes from './ActionForms.module.css';

const ActionForms = ({
  handleStudentFormInput,
  handleAddStudent,
  handleEditStudent,
  handleDeleteStudent,
  actionState,
  studentFormData,
  availableSelections
}) => {
  return (
    <div className={classes.action_form}>
      {actionState.action === 'add' && actionState.show && (
        <StudentForm
          formType="add"
          handleFormSubmission={handleAddStudent}
          handleStudentFormInput={handleStudentFormInput}
          studentFormData={studentFormData}
          availableSelections={availableSelections}
        />
      )}
      {actionState.action === 'edit' && actionState.show && (
        <StudentForm
          formType="edit"
          handleFormSubmission={handleEditStudent}
          handleStudentFormInput={handleStudentFormInput}
          studentFormData={studentFormData}
          availableSelections={availableSelections}
        />
      )}
      {actionState.action === 'delete' && actionState.show && (
        <StudentDeleteForm handleDeleteStudent={handleDeleteStudent} />
      )}
    </div>
  );
};

export default ActionForms;
