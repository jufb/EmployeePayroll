import { Navbar } from "react-bootstrap";

export function Header() {
    return (
        <header className="App-header">
            <Navbar sticky="top" bg="dark" variant="dark">
                <Navbar.Brand>Employee Payroll</Navbar.Brand>
            </Navbar>
        </header>
    );
}