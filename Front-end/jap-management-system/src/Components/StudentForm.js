import { Button, Form, Row, Col } from 'react-bootstrap';

const StudentForm = ({
  formType,
  handleFormSubmission,
  handleStudentFormInput,
  studentFormData,
  availableSelections
}) => {
  return (
    <Form onSubmit={handleFormSubmission}>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalFirstName">
        <Form.Label column sm={2}>
          First name
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="firstName"
            type="text"
            placeholder="Enter first name"
            onChange={handleStudentFormInput}
            value={studentFormData.firstName}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalLastName">
        <Form.Label column sm={2}>
          Last name
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="lastName"
            type="text"
            placeholder="Enter last name"
            onChange={handleStudentFormInput}
            value={studentFormData.lastName}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalEmail">
        <Form.Label column sm={2}>
          Email
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="email"
            type="email"
            placeholder="Enter email"
            onChange={handleStudentFormInput}
            value={studentFormData.email}
          />
        </Col>
      </Form.Group>

      <Form.Group
        as={Row}
        className="mb-3"
        controlId="formHorizontalStudentStatus"
      >
        <Form.Label column sm={2}>
          Student status
        </Form.Label>
        <Col sm={10}>
          <Form.Select
            name="status"
            className="form-select"
            defaultValue="Status"
            aria-label="Default select example"
            required
            onChange={handleStudentFormInput}
          >
            <option value="Status">Status</option>
            <option value="InProgram">InProgram</option>
            <option value="Success">Success</option>
            <option value="Failed">Failed</option>
            <option value="Extended">Extended</option>
          </Form.Select>
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalSelection">
        <Form.Label column sm={2}>
          Selection
        </Form.Label>
        <Col sm={10}>
          <Form.Select
            name="selection"
            className="form-select"
            defaultValue="Selection"
            aria-label="Default select example"
            required
            onChange={handleStudentFormInput}
          >
            <option value="Selection">Selection</option>
            {availableSelections.map(s => {
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
          {formType === 'add' && <Button type="submit">Add</Button>}
          {formType === 'edit' && (
            <Button type="submit" variant="success">
              Edit
            </Button>
          )}
        </Col>
      </Form.Group>
    </Form>
  );
};

export default StudentForm;
