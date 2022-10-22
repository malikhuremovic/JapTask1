import React from 'react';

import StudentForm from './Student/StudentForm';
import closeIcon from '../Assets/closeIcon.png';
import StudentDeleteForm from './Student/StudentDeleteForm';
import SelectionForm from './Selection/SelectionForm';
import SelectionDeleteForm from './Selection/SelectionDeleteForm';

import classes from './FormModal.module.css';
import LectureForm from './Lecture/LectureForm';
import LectureDeleteForm from './Lecture/LectureDeleteForm';

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
          <img src={closeIcon} alt="close button" onClick={handleState} />
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
          {formModel === 'lecture' && (
            <React.Fragment>
              {formType !== 'delete' && (
                <LectureForm
                  formType={formType}
                  handleFormSubmission={handleFormSubmission}
                  handleLectureFormInput={handleFormInput}
                  lectureFormData={formData}
                />
              )}
              {formType === 'delete' && (
                <LectureDeleteForm handleDeleteSelection={handleDelete} />
              )}
            </React.Fragment>
          )}
        </div>
      </div>
    </div>
  );
};

export default FormModal;
