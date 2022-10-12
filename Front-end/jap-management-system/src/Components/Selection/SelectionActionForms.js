import SelectionDeleteForm from './SelectionDeleteForm';
import SelectionForm from './SelectionForm';

import classes from '../Students/ActionForms.module.css';

const SelectionActionForms = ({
  handleSelectionFormInput,
  handleAddSelection,
  handleEditSelection,
  handleDeleteSelection,
  actionState,
  selectionFormData,
  availablePrograms
}) => {
  return (
    <div className={classes.action_form}>
      {actionState.action === 'add' && actionState.show && (
        <SelectionForm
          formType="add"
          handleFormSubmission={handleAddSelection}
          handleSelectionFormInput={handleSelectionFormInput}
          selectionFormData={selectionFormData}
          availablePrograms={availablePrograms}
        />
      )}
      {actionState.action === 'edit' && actionState.show && (
        <SelectionForm
          formType="edit"
          handleFormSubmission={handleEditSelection}
          handleSelectionFormInput={handleSelectionFormInput}
          selectionFormData={selectionFormData}
          availablePrograms={availablePrograms}
        />
      )}
      {actionState.action === 'delete' && actionState.show && (
        <SelectionDeleteForm handleDeleteSelection={handleDeleteSelection} />
      )}
    </div>
  );
};

export default SelectionActionForms;
