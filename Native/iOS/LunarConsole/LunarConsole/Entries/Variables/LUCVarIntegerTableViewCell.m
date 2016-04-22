//
//  LUCVarIntegerTableViewCell.m
//  LunarConsole
//
//  Created by Alex Lementuev on 4/20/16.
//  Copyright © 2016 Space Madness. All rights reserved.
//

#import "LUCVarIntegerTableViewCell.h"

#import "Lunar.h"

@implementation LUCVarIntegerTableViewCell

#pragma mark -
#pragma mark Inheritance

- (BOOL)isValidInputText:(NSString *)text
{
    return LUStringTryParseInteger(text, NULL);
}

@end
