//
//  UITestCaseBase.m
//  LunarConsoleApp
//
//  Created by Alex Lementuev on 3/23/16.
//  Copyright © 2016 Space Madness. All rights reserved.
//

#import "UITestCaseBase.h"

@implementation UITestCaseBase

- (void)app:(XCUIApplication *)app tapButton:(NSString *)title
{
    XCTAssertTrue(app.buttons[title].exists);
    [app.buttons[title] tap];
}

- (void)app:(XCUIApplication *)app tapSwitch:(NSString *)title
{
    XCTAssertTrue(app.switches[title].exists);
    [app.switches[title] tap];
}

- (void)app:(XCUIApplication *)app tapTextField:(NSString *)title
{
    XCUIElement *element = app.textFields[title];
    [self waitUntilElementExists:element timeout:5.0];
    [element tap];
}

- (void)appDeleteText:(XCUIApplication *)app
{
    [[self appDeleteKey:app] pressForDuration:2.5];
}

- (void)appDeleteChar:(XCUIApplication *)app
{
    [[self appDeleteKey:app] tap];
}

- (void)app:(XCUIApplication *)app textField:(NSString *)textField enterText:(NSString *)text
{
    // find element
    XCUIElement *element = app.textFields[textField];
    
    // tap element
    [element tap];
    
    // delete old text
    [self appDeleteText:app];
    
    // type new text
    [element typeText:text];
    
    // hit 'return'
    [[self appReturnButton:app] tap];
}

- (XCUIElement *)appDeleteKey:(XCUIApplication *)app
{
    if (app.keys[@"Delete"].exists) return app.keys[@"Delete"];
    if (app.keys[@"delete"].exists) return app.keys[@"delete"];
    
    XCTFail(@"Can't resolve 'delete' button");
    return nil;
}

- (XCUIElement *)appReturnButton:(XCUIApplication *)app
{
    if (app.buttons[@"Return"].exists) return app.buttons[@"Return"];
    if (app.buttons[@"return"].exists) return app.buttons[@"return"];
    
    XCTFail(@"Can't resolve 'return' button");
    return nil;
}

@end
