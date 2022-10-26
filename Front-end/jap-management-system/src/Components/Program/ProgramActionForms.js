import FormModal from '../FormModal';

import classes from '../Style/ActionForms.module.css';

const ProgramActionForms = ({
  formModel,
  handleProgramFormInput,
  handleAddProgram,
  handleEditProgram,
  handleDeleteProgram,
  handleAddState,
  handleEditState,
  handleDeleteState,
  actionState,
  programFormData
}) => {
  return (
    <div className={classes.action_form}>
      {actionState.action === 'add' && actionState.show && (
        <FormModal
          title="Add program"
          formType="add"
          formModel={formModel}
          handleState={handleAddState}
          handleFormSubmission={handleAddProgram}
          handleFormInput={handleProgramFormInput}
          formData={programFormData}
        />
      )}
      {actionState.action === 'edit' && actionState.show && (
        <FormModal
          title="Edit program"
          formType="edit"
          formModel={formModel}
          handleState={handleEditState}
          handleFormSubmission={handleEditProgram}
          handleFormInput={handleProgramFormInput}
          formData={programFormData}
        />
      )}
      {actionState.action === 'delete' && actionState.show && (
        <FormModal
          title="Delete program"
          formType="delete"
          formModel={formModel}
          handleState={handleDeleteState}
          handleDelete={handleDeleteProgram}
        />
      )}
    </div>
  );
};

export default ProgramActionForms;
