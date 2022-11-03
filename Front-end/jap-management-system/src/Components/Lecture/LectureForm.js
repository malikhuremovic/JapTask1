import { Button, Form, Row, Col } from 'react-bootstrap';

const LectureForm = ({ formType, handleFormSubmission, handleLectureFormInput, lectureFormData }) => {
  console.log(lectureFormData);
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
            onChange={handleLectureFormInput}
            value={lectureFormData.name}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalLastName">
        <Form.Label column sm={2}>
          Short Description
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            as="textarea"
            rows={3}
            name="description"
            placeholder="Enter short lecture description"
            onChange={handleLectureFormInput}
            value={lectureFormData.description}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalEmail">
        <Form.Label column sm={2}>
          Lecture's URLs
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            as="textarea"
            rows={3}
            name="url"
            placeholder="Enter multiple URLs seperated by a comma"
            onChange={handleLectureFormInput}
            value={lectureFormData.url}
          />
        </Col>
      </Form.Group>

      <Form.Group as={Row} className="mb-3" controlId="formHorizontalFirstName">
        <Form.Label column sm={2}>
          Number of hours to complete
        </Form.Label>
        <Col sm={10}>
          <Form.Control
            name="expectedHours"
            type="number"
            placeholder="Expected number of hours: "
            onChange={handleLectureFormInput}
            value={lectureFormData.expectedHours}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3" controlId="formHorizontalFirstName">
        <Form.Label column sm={2}>
          Should this lecture be marked as an event?
        </Form.Label>
        <Col sm={10}>
          <Form.Check
            name="isEvent"
            label="Yes, this is an event"
            onChange={handleLectureFormInput}
            checked={lectureFormData.isEvent ? true : false}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3">
        <Col sm={{ span: 10, offset: 2 }}>
          {formType === 'add' && (
            <Button style={{ position: 'relative', left: '-20%' }} type="submit">
              Add new lecture
            </Button>
          )}
          {formType === 'edit' && (
            <Button style={{ position: 'relative', left: '-20%' }} type="submit" variant="success">
              Edit lecture
            </Button>
          )}
        </Col>
      </Form.Group>
    </Form>
  );
};

export default LectureForm;
