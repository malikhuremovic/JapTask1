import FormModal from '../FormModal';

import classes from '../Style/ActionForms.module.css';

const LectureActionForm = ({
  formModel,
  handleLectureFormInput,
  handleAddSelection,
  handleEditSelection,
  handleDeleteSelection,
  handleAddState,
  handleEditState,
  handleDeleteState,
  actionState,
  lectureFormData,
  availablePrograms
}) => {
  return (
    <div className={classes.action_form}>
      {actionState.action === 'add' && actionState.show && (
        <FormModal
          title="Add lecture"
          formType="add"
          formModel={formModel}
          handleState={handleAddState}
          handleFormSubmission={handleAddSelection}
          handleFormInput={handleLectureFormInput}
          formData={lectureFormData}
          availableItems={availablePrograms}
        />
      )}
      {actionState.action === 'edit' && actionState.show && (
        <FormModal
          title="Edit lecture"
          formType="edit"
          formModel={formModel}
          handleState={handleEditState}
          handleFormSubmission={handleEditSelection}
          handleFormInput={handleLectureFormInput}
          formData={lectureFormData}
          availableItems={availablePrograms}
        />
      )}
      {actionState.action === 'delete' && actionState.show && (
        <FormModal
          title="Delete lecture"
          formType="delete"
          formModel={formModel}
          handleState={handleDeleteState}
          handleDelete={handleDeleteSelection}
        />
      )}
    </div>
  );
};

export default LectureActionForm;
