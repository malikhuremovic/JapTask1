import FormModal from '../FormModal';

import classes from '../Style/ActionForms.module.css';

const SelectionActionForms = ({
  formModel,
  handleSelectionFormInput,
  handleAddSelection,
  handleEditSelection,
  handleDeleteSelection,
  handleAddState,
  handleEditState,
  handleDeleteState,
  actionState,
  selectionFormData,
  availablePrograms
}) => {
  return (
    <div className={classes.action_form}>
      {actionState.action === 'add' && actionState.show && (
        <FormModal
          title="Add selection"
          formType="add"
          formModel={formModel}
          handleState={handleAddState}
          handleFormSubmission={handleAddSelection}
          handleFormInput={handleSelectionFormInput}
          formData={selectionFormData}
          availableItems={availablePrograms}
        />
      )}
      {actionState.action === 'edit' && actionState.show && (
        <FormModal
          title="Edit selection"
          formType="edit"
          formModel={formModel}
          handleState={handleEditState}
          handleFormSubmission={handleEditSelection}
          handleFormInput={handleSelectionFormInput}
          formData={selectionFormData}
          availableItems={availablePrograms}
        />
      )}
      {actionState.action === 'delete' && actionState.show && (
        <FormModal
          title="Delete selection"
          formType="delete"
          formModel={formModel}
          handleState={handleDeleteState}
          handleDelete={handleDeleteSelection}
        />
      )}
    </div>
  );
};

export default SelectionActionForms;
