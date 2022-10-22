import React from 'react';
import classes from './FormModal.module.css';
import StudentForm from './Students/StudentForm';
import closeIcon from '../Assets/closeIcon.png';
import StudentDeleteForm from './Students/StudentDeleteForm';
import SelectionForm from './Selection/SelectionForm';
import SelectionDeleteForm from './Selection/SelectionDeleteForm';

const FormModal = ({
  title,
  formType,
  formModel,
  handleFormSubmission,
  handleFormInput,
  formData,
  handleState,
  handleDelete,
  availableItems
}) => {
  return (
    <div className={classes.backdrop}>
      <div className={classes.modal}>
        <div className={classes.formTitle}>
          <h4>{title}</h4>
          <img src={closeIcon} onClick={handleState} />
        </div>
        <div className={classes.modalForm}>
          {formModel === 'student' && (
            <React.Fragment>
              {formType !== 'delete' && (
                <StudentForm
                  formType={formType}
                  handleFormSubmission={handleFormSubmission}
                  handleStudentFormInput={handleFormInput}
                  studentFormData={formData}
                  availableSelections={availableItems}
                />
              )}
              {formType === 'delete' && (
                <StudentDeleteForm handleDeleteStudent={handleDelete} />
              )}
            </React.Fragment>
          )}
          {formModel === 'selection' && (
            <React.Fragment>
              {formType !== 'delete' && (
                <SelectionForm
                  formType={formType}
                  handleFormSubmission={handleFormSubmission}
                  handleSelectionFormInput={handleFormInput}
                  selectionFormData={formData}
                  availablePrograms={availableItems}
                />
              )}
              {formType === 'delete' && (
                <SelectionDeleteForm handleDeleteSelection={handleDelete} />
              )}
            </React.Fragment>
          )}
        </div>
      </div>
    </div>
  );
};

export default FormModal;
