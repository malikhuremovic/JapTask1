import { Button, Form, Row, Col } from 'react-bootstrap';

const SelectionForm = ({
  formType,
  handleFormSubmission,
  handleSelectionFormInput,
  selectionFormData,
  availablePrograms
}) => {
  return (
    <Form onSubmit={handleFormSubmission}>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalFirstName">
        <Form.Label column sm={2}>
          Name
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="name"
            type="text"
            placeholder="Enter name"
            onChange={handleSelectionFormInput}
            value={selectionFormData.name}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalLastName">
        <Form.Label column sm={2}>
          Date Start
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="dateStart"
            type="date"
            placeholder="Enter start date"
            onChange={handleSelectionFormInput}
            value={selectionFormData.dateStart.split('T')[0]}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalEmail">
        <Form.Label column sm={2}>
          Date End
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="dateEnd"
            type="date"
            placeholder="Enter email"
            onChange={handleSelectionFormInput}
            value={selectionFormData.dateEnd.split('T')[0]}
          />
        </Col>
      </Form.Group>

      <Form.Group
        as={Row}
        className="mb-3"
        controlId="formHorizontalStudentStatus"
      >
        <Form.Label column sm={2}>
          Selection status
        </Form.Label>
        <Col sm={10}>
          <Form.Select
            name="status"
            className="form-select"
            defaultValue={
              selectionFormData.status ? selectionFormData.status : 'none'
            }
            aria-label="Default select example"
            required
            onChange={handleSelectionFormInput}
          >
            {!selectionFormData.status && (
              <option value="none">Select Status</option>
            )}
            <option value="Active">Active</option>
            <option value="Completed">Completed</option>
          </Form.Select>
        </Col>
      </Form.Group>

      <Form.Group
        as={Row}
        className="mb-3"
        controlId="formHorizontalStudentStatus"
      >
        <Form.Label column sm={2}>
          Program
        </Form.Label>
        <Col sm={10}>
          <Form.Select
            name="program"
            className="form-select"
            defaultValue={
              selectionFormData.japProgramId
                ? selectionFormData.japProgramId
                : 'none'
            }
            aria-label="Default select example"
            required
            onChange={handleSelectionFormInput}
          >
            {!selectionFormData.japProgramId && (
              <option value="none">Select Program</option>
            )}
            {availablePrograms.map(s => {
              return (
                <option key={s.id} value={s.id}>
                  {' '}
                  {s.name}
                </option>
              );
            })}
          </Form.Select>
        </Col>
      </Form.Group>

      <Form.Group as={Row} className="mb-3">
        <Col sm={{ span: 10, offset: 2 }}>
          {formType === 'add' && (
            <Button style={{ width: 70 }} type="submit">
              Add
            </Button>
          )}
          {formType === 'edit' && (
            <Button style={{ width: 70 }} type="submit" variant="success">
              Edit
            </Button>
          )}
        </Col>
      </Form.Group>
    </Form>
  );
};

export default SelectionForm;
