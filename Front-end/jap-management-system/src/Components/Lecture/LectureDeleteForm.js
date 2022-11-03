import { Form, Row, Col, Button } from 'react-bootstrap';

const LectureDeleteForm = ({ handleDeleteSelection }) => {
  return (
    <Form onSubmit={handleDeleteSelection}>
      <Row className="align-items-center">
        <Col xs="auto">
          <Form.Label>
            Are you aware that by performing this action, a lecture will be removed from the JAP platform?
          </Form.Label>
        </Col>
        <Col xs="auto">
          <Button type="submit" variant="danger" className="mb-2">
            I am aware, Delete lecture
          </Button>
        </Col>
      </Row>
    </Form>
  );
};

export default LectureDeleteForm;
